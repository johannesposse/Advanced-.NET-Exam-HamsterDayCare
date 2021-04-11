using BackEnd.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BackEnd
{
    public class HamsterDayCare 
    {
        private Ticker ticker = new Ticker(); //en ny instans av ticker som driver simuleringen
        public static event EventHandler<PrintEventArgs> PrintEvent; //event för att skriva ut till skärm vad som händer varje tick
        public event EventHandler<ReportEventArgs> ReportEvent; //event för att skapa rapporter
        private HamsterDayCareContext HDCon = new HamsterDayCareContext(); //ny instans av hamsterdaycarecontext som gör det möjligt att koppla upp mot databasen
        private DateTime Date; //en datetime som sätts varje tick


        public bool InitilizeDatabase(out bool dbHasData) //metod för att initialisera databasen, om det inte redan finns den grund data som behövs
        {
            dbHasData = true; //bool för att kolla om databasen har grund data


            if (!HDCon.Cages.Any()) //kollar om det finns några burar, gör det inte det skapas 10st burar
            {
                for (int i = 0; i < 10; i++)
                {
                    var tempCage = new Cage(i);
                    HDCon.Cages.Add(tempCage);
                    HDCon.SaveChanges();
                }
            }

            if (!HDCon.ExerciseArea.Any()) //kollar om det finns ett träningsområde, för det inte det så skapas ett träningsområde
            {
                var tempExerciseArea = new ExerciseArea();
                HDCon.ExerciseArea.Add(tempExerciseArea);
                HDCon.SaveChanges();
            }

            if (!HDCon.Hamsters.Any()) //kollar om det finns hamstrar, gör det inte det skapas hamstrar
            {
                List<string> hamsterData = File.ReadAllLines(@"..\..\..\..\Hamsterlista30.csv").ToList(); //läser in från fil till en string lista

                for (int i = 0; i < hamsterData.Count; i++) //loopar igenom listan
                {
                    string[] data = hamsterData[i].Split(";"); //splittar varje rad till en array
                    bool isFemale = true; //sätter en bool för att kolla kön till true som default
                    if (data[2] == "M") //om datan som läses in är en kille sätts boolen till false
                    {
                        isFemale = false;
                    }
                    var tempHamster = new Hamster(data[0], data[3], Math.Round(decimal.Parse(data[1]) / 12, 1), isFemale); //skapar en ny hamster
                    HDCon.Hamsters.Add(tempHamster); //lägger till hamstern i databasen
                    HDCon.SaveChanges(); //sparar
                }
            }

            if (!HDCon.Cages.Any() ^ !HDCon.ExerciseArea.Any() ^ !HDCon.Hamsters.Any()) //kollar om cages, träningsområde och hamsters har någon data
            {
                dbHasData = false; //har dom inte det sätts boolsen till false, annars är den true
            }

            HDCon.SaveChanges(); //sparar ändringar

            return dbHasData; //retunerar boolen
        }
        public void StartSimulation(int days, int ticksPerSecond) //metod som startar igång hela simuleringen, tar emot antal dagar och hastigheten till simuleringen
        {
            Reset(); //nollställer alla värden, ifall en simulering skulle ha avbrutits så sätter den rätt värden för att kunna skapa en ny simulering utan problem
            ticksPerSecond = 1000 / ticksPerSecond; //delar ticksperSecond på 1000 för att kunna få fram hur många millesekunder ett tick ska vara
            ticker.Tick += StartThreads; //sätter StartThreads som lyssnarna för varje gång ett tick körs i simuleringen
            ticker.StartTick(ticksPerSecond, days); //startar whileloopen som driver simuleringen, skickar in hastighet och antal dagar
        }
        private async void StartThreads(object sender, TickEventArgs e) //async metod som anropas på varje tick
        {

            if (e.Date.TimeOfDay == TimeSpan.Parse("17:00:00")) //kollar om kl är 17, då är dagen över
            {
                e.IsPaused = true; //simuleringen pausas
                Date = e.Date; //date sätts till datumet simuleringen är på

                var checkOutTask = CheckOutHamstersForTheDay(); //skapar en task som skickar hem hamstrarna för dagen
                await checkOutTask; //awaitar tasken

                PrintEvent?.Invoke(this, new PrintEventArgs(Print(), e.Date)); //invokar ett event som skriver ut vad som hänt detta tick
                ReportEvent?.Invoke(this, new ReportEventArgs(HDCon.Hamsters.ToList(), HDCon.ActivityLogs.ToList())); //invokar ett event som genererar och skriver ut rapport för dagen

                var logs = HDCon.ActivityLogs;
                HDCon.ActivityLogs.RemoveRange(logs); //tömmer logs i databasen
                HDCon.SaveChanges(); //sparar ändringar

                e.Date = e.Date.AddHours(13.9); //lägger till 13.9h på simuleringen för att starta en ny dag
                e.IsPaused = false; //startar simuleringen igen


            }
            else if (e.Date.Hour >= 7 & e.Date.TimeOfDay <= TimeSpan.Parse("17:00:00")) //om kl är mellan 07.00 och 17.00
            {
                Date = e.Date; //date sätts till datumet simuleringen är på

                if (e.Date.TimeOfDay == TimeSpan.Parse("07:00:00")) //om kl är 07.00
                {
                    var addToCageTask = AddHamstersToCages(); //skapar en task för att checka in hamstrarna för dagen, behöver endast göras en gång per dag
                    await addToCageTask; //awaitar tasken
                }


                var retrieveFromExerciseTask = RetreiveHamstersFromExtersiceArea(); //skapar en task som plockar ut hamstrar från träningsområdet
                var addToExerciseTask = AddHamstersToExerciseArea(); //skapar en task som lägger till hamstrar till träningsområdet

                await retrieveFromExerciseTask; //awaitar tasken
                await addToExerciseTask; //awaitar tasken
                PrintEvent?.Invoke(this, new PrintEventArgs(Print(), e.Date)); //invokar ett event som skriver ut vad som hänt detta tick
            }
        }
        private async Task AddHamstersToExerciseArea() //async metod för att lägga till hamstrar till träningsområdet
        {
            var hamsters = HDCon.Hamsters.Where(x => x.CageID != null).OrderBy(x => x.LastExercise).ToList(); //hämtar hamstrar som är i en bur, och sorterar dom på den som väntat längs på träning kommer först
            var exerciseArea = HDCon.ExerciseArea.First(); //hämtar ut det träningsområdet (finns bara ett)
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++) //loopar igenom alla hamstrar
            {
                if (exerciseArea.Hamsters.Count < exerciseArea.MaxSize) //kollar om det finns plats att lägga till en hamster
                {
                    if (!exerciseArea.Hamsters.Any() | exerciseArea.Hamsters.Select(x => x.IsFemale).FirstOrDefault() == hamsters[i].IsFemale) //kollar om det antingen är tommt eller om det finns hamstrar av samma kön
                    {
                        var cage = cages.Where(x => x.Hamsters.Contains(hamsters[i])).FirstOrDefault(); //hämtar ut den buren som hamstern var i
                        if (cage.Hamsters.Count == 1) //om det var den sista hamster i buren så nollställs hasFemale boolen till sitt default värde
                            cage.HasFemale = false;

                        var log = logs.Where(x => x.ActivityName == "Cage: " + hamsters[i].CageID.ToString() & x.HamsterID == hamsters[i].ID & x.EndDate == null).FirstOrDefault(); //uppdaterar loggen att hamstern blev utplockad från sin bur
                        log.EndDate = Date; //sätter slutdatum i loggen
                        hamsters[i].LastExercise = Date; //sätter att hamstern började träna vid denna tid
                        hamsters[i].CageID = null; //tar ut hamstern ur buren
                        exerciseArea.Hamsters.Add(hamsters[i]); //lägger till hamstern i träningsområdet
                        logs.Add(new ActivityLog("Exercise", Date, hamsters[i].ID)); //gör ett nytt inlägg i loggen att hamstern har börjat träna
                        HDCon.SaveChanges(); //sparar ändringar
                    }
                }
                else //om träningsområdet är fullt avbryts loopen
                {
                    break;
                }
            }
            await Task.CompletedTask; //awaitar tasken
        }
        private async Task RetreiveHamstersFromExtersiceArea() //async metod för att hämta hamstrar från träningsområde
        {

            var exerciseArea = HDCon.ExerciseArea.First(); // hämtar ut det träningsområdet(finns bara ett)
            var hamsters = exerciseArea.Hamsters.Where(x => x.LastExercise.Value.Hour + 1 == Date.Hour).ToList(); //hämtar ut de hamstrar som tränat i en timma
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++) //loopar igenom alla hamstrar
            {
                //hittar en bur där hamstern kan stoppas in, som har plats, har samma kön eller är tom
                var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1))); 

                if (cage != null) //om buren finns
                {
                    cage.Hamsters.Add(hamsters[i]); //lägger till hamstern i buren
                    cage.HasFemale = hamsters[i].IsFemale; //sätter hasFemaleBoolen till hamsterns kön
                    hamsters[i].ExerciseAreaID = null; //plockar ut hamstern ur träningsområdet
                    var log = logs.Where(x => x.HamsterID == hamsters[i].ID & x.ActivityName == "Exercise" & x.EndDate == null).FirstOrDefault(); //hittar i loggen där hamstern började träna
                    log.EndDate = Date; //uppdaterar loggen att hamstern slutat träna
                    logs.Add(new ActivityLog("Cage: " + cage.ID.ToString(), Date, hamsters[i].ID)); //gör ett nytt inlägg i loggen att hamstern las in i en bur
                    HDCon.SaveChanges(); //sparar ändringar
                }
            }

            await Task.CompletedTask; //awaitar tasken
        }
        private async Task AddHamstersToCages() //async metod för att lägga till hamstrar i burar i börhan på dagen
        {
            var hamsters = HDCon.Hamsters.Shuffle().ToList(); //blandar alla hamstrar, för att få olika resultat 
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++) //loopar igenom alla hamstrar
            {
                if (hamsters[i].ExerciseAreaID == null & hamsters[i].CageID == null) //kollar så att dom inte redan är i en bur eller tränar
                {
                    //hittar en bur där hamstern kan stoppas in, som har plats, har samma kön eller är tom
                    var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1)));

                    if (cage != null) //om buren finns
                    {
                        cage.Hamsters.Add(hamsters[i]); //lägger till hamstern i buren
                        cage.HasFemale = hamsters[i].IsFemale; //sätter hasFemaleBoolen till hamsterns kön
                        logs.Add(new ActivityLog("Checked In for The Day", Date, hamsters[i].ID)); //gör ett nytt inlägg i loggen om att hamstern checkat in för dagen
                        logs.Add(new ActivityLog("Cage: " + cage.ID.ToString(), Date, hamsters[i].ID)); //gör ett nytt inlägg i loggen att hamstern las in i en bur

                        hamsters[i].CheckedInTime = Date; //sätter att hamstern checkades in vid denna tid

                        HDCon.SaveChanges(); //sparar ändringar
                    }
                }
            }

            await Task.CompletedTask; //awaitar taksen
        }
        public async Task CheckOutHamstersForTheDay() //async metod för att skicka hem hamstrarna när dagen är slut
        {
            var logs = HDCon.ActivityLogs;

            foreach (var ham in HDCon.Hamsters) //går igenom alla hamstrar
            {
                var log = logs.Where(x => x.HamsterID == ham.ID & x.EndDate == null); //hittar alla loggar för hamstern som inte har ett slutdatum
                if (log != null) //kollar så att det finns loggar
                {
                    foreach (var l in log) //går igenom alla loggar
                    {
                        l.EndDate = Date; //sätter slutdatumet
                    }
                }
                ham.CageID = null; //plockar ut hamster ur buren
                ham.ExerciseAreaID = null; //plockar ut hamstern ur träningsområdet
                ham.CheckedInTime = null; //nollställer incheckad tid
                ham.LastExercise = null; //nollställer senaste tid den tränat
            }

            foreach (var c in HDCon.Cages) //loopar igenom och nollställer alla burar
            {
                c.Hamsters.Clear();
                c.HasFemale = false; //sätter hasFemale bool till sitt default värde
            }

            HDCon.SaveChanges(); //sparar ändringar
            await Task.CompletedTask; //awaitar task
        }

        private void Reset() //metod för att nollställa databasen vid uppstart av ny simulering vid eventuell krash
        {

            HDCon.ActivityLogs.RemoveRange(HDCon.ActivityLogs); //tar bort alla activitylogs
            HDCon.SaveChanges(); //sparar ändringar

            foreach (var ham in HDCon.Hamsters) //loopar igenom alla hamstrar och nollställer dom
            {
                ham.CageID = null;
                ham.ExerciseAreaID = null;
                ham.CheckedInTime = null;
                ham.LastExercise = null;
            }

            foreach (var c in HDCon.Cages) //loopar igenom alla burar och nollställer dom
            {

                c.Hamsters.Clear();
                c.HasFemale = false;
            }

            HDCon.SaveChanges(); //sparar ändringar
        }
        private string Print()
        {
            var print = new StringBuilder();


            var hamsters = HDCon.Hamsters.OrderBy(x => x.CageID); //hämtar ut alla hamstrar och sorterar dom efter vilken bur dom är i 

            //lägger till column name
            print.Append($"{"CID",-7}{"EID",-10}{"Name",-15}\t{"Age",-10}\t{"Sex",-10}\t\t{"Owner",-30}   \t\t{"CheckedIn",-40}\t{"Exersiced",-40}" + Environment.NewLine + Environment.NewLine);

            foreach (var h in hamsters) //loopar igenom alla hamstrar
            {
                string female = "Female"; //defualt värde
                string cageID = h.CageID.ToString();
                string ExID = h.ExerciseAreaID.ToString(); ;
                if (!h.IsFemale) //kollar om det är en hane
                    female = "Male"; 
                if (h.CageID == null) //om den inte är i en bur
                    cageID = "";
                if (h.ExerciseAreaID == null) //om den inte är och tränar
                    ExID = "";

                //lägger till införmation om hamstern
                //print.Append($"{cageID,-3}{ExID,-3}{h.Name,-15}\t{h.Age,-10}\t{female,-20}\t{h.Ownername,-25}   \t\t{h.CheckedInTime,-40}\t{h.LastExercise,-40}" + Environment.NewLine);
                print.Append($"{cageID,-10}{ExID,-10}{h.Name,-15}\t{h.Age,-10}\t{female,-20}\t{h.Ownername,-25}   \t\t{h.CheckedInTime,-40}\t{h.LastExercise,-40}" + Environment.NewLine);
            }

            PrintEvent?.Invoke(this, new PrintEventArgs(print.ToString(), Date)); //invokar ett event som skriver ut vad som hänt detta tick
            return print.ToString(); //retunerar stringbuildern som en string
        }

        public string[] ShowPreviousResults() //metod för att kolla på föregående rapporter
        {

            if (!Directory.Exists(@"..\..\..\..\Logs")) //kollar om Logs foldern finns
            {
                throw new Exception("The path does not exist, please run the simulation once to create it..."); //gör den inte det skapas ett felmeddelande
            }

            string[] documents = System.IO.Directory.GetFiles("../../../../Logs/"); //läser in alla filnamn som finns till en string array


            for (int i = 0; i < documents.Length; i++) //loopar igenom arrayen
            {
                documents[i] = documents[i].Replace("../../../../Logs/", ""); //snyggar till filnamnet
            }


            return documents; //retunerar arrayen med alla filnamn
        }

    }
}

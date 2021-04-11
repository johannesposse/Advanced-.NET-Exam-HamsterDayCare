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
            ticksPerSecond = 1000 / ticksPerSecond; //
            ticker.Tick += StartThreads;
            ticker.StartTick(ticksPerSecond, days);
        }
        private async void StartThreads(object sender, TickEventArgs e)
        {

            if (e.Date.TimeOfDay == TimeSpan.Parse("17:00:00"))
            {
                e.IsPaused = true;
                Date = e.Date;

                var checkOutTask = CheckOutHamstersForTheDay();
                await checkOutTask;

                ReportEvent?.Invoke(this, new ReportEventArgs(HDCon.Hamsters.ToList(), HDCon.ActivityLogs.ToList()));

                var logs = HDCon.ActivityLogs;
                HDCon.ActivityLogs.RemoveRange(logs);
                HDCon.SaveChanges();

                e.Date = e.Date.AddHours(13.9);
                e.IsPaused = false;


            }
            else if (e.Date.Hour >= 7 & e.Date.TimeOfDay <= TimeSpan.Parse("17:00:00"))
            {
                Date = e.Date;

                if (e.Date.TimeOfDay == TimeSpan.Parse("07:00:00"))
                {
                    var addToCageTask = AddHamstersToCages();
                    await addToCageTask;
                }

                PrintEvent?.Invoke(this, new PrintEventArgs(Print(), e.Date));

                var retrieveFromExerciseTask = RetreiveHamstersFromExtersiceArea();
                var addToExerciseTask = AddHamstersToExerciseArea();

                await retrieveFromExerciseTask;
                await addToExerciseTask;
            }
        }
        private async Task AddHamstersToExerciseArea()
        {
            var hamsters = HDCon.Hamsters.Where(x => x.CageID != null).OrderBy(x => x.LastExercise).ToList();
            var exerciseArea = HDCon.ExerciseArea.First();
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++)
            {
                if (hamsters[i].Name == "Starlight" & Date.Hour == 13)
                {

                }
                if (exerciseArea.Hamsters.Count < exerciseArea.MaxSize)
                {
                    if (!exerciseArea.Hamsters.Any() | exerciseArea.Hamsters.Select(x => x.IsFemale).FirstOrDefault() == hamsters[i].IsFemale)
                    {
                        var cage = cages.Where(x => x.Hamsters.Contains(hamsters[i])).FirstOrDefault();
                        if (cage.Hamsters.Count == 1)
                            cage.HasFemale = false;

                        var log = logs.Where(x => x.ActivityName == "Cage: " + hamsters[i].CageID.ToString() & x.HamsterID == hamsters[i].ID & x.EndDate == null).FirstOrDefault();
                        log.EndDate = Date;
                        hamsters[i].LastExercise = Date;
                        hamsters[i].CageID = null;
                        exerciseArea.Hamsters.Add(hamsters[i]);
                        logs.Add(new ActivityLog("Exercise", Date, hamsters[i].ID));
                        HDCon.SaveChanges();
                    }
                }
                else
                {
                    break;
                }
            }
            await Task.CompletedTask;
        }
        private async Task RetreiveHamstersFromExtersiceArea()
        {

            var exerciseArea = HDCon.ExerciseArea.First();
            var hamsters = exerciseArea.Hamsters.Where(x => x.LastExercise.Value.Hour + 1 == Date.Hour).ToList();
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++)
            {
                var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1)));

                if (cage != null)
                {
                    cage.Hamsters.Add(hamsters[i]);
                    cage.HasFemale = hamsters[i].IsFemale;
                    hamsters[i].ExerciseAreaID = null;
                    var log = logs.Where(x => x.HamsterID == hamsters[i].ID & x.ActivityName == "Exercise" & x.EndDate == null).FirstOrDefault();
                    log.EndDate = Date;
                    logs.Add(new ActivityLog("Cage: " + cage.ID.ToString(), Date, hamsters[i].ID));
                    HDCon.SaveChanges();
                }
            }

            await Task.CompletedTask;
        }
        private async Task AddHamstersToCages()
        {
            var hamsters = HDCon.Hamsters.Shuffle().ToList();
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++)
            {
                if (hamsters[i].ExerciseAreaID == null & hamsters[i].CageID == null)
                {
                    var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1)));

                    if (cage != null)
                    {
                        cage.Hamsters.Add(hamsters[i]);
                        cage.HasFemale = hamsters[i].IsFemale;
                        logs.Add(new ActivityLog("Checked In for The Day", Date, hamsters[i].ID));
                        logs.Add(new ActivityLog("Cage: " + cage.ID.ToString(), Date, hamsters[i].ID));

                        hamsters[i].CheckedInTime = Date;

                        HDCon.SaveChanges();
                    }
                }
            }

            await Task.CompletedTask;
        }
        public async Task CheckOutHamstersForTheDay()
        {
            var logs = HDCon.ActivityLogs;

            foreach (var ham in HDCon.Hamsters)
            {
                var log = logs.Where(x => x.HamsterID == ham.ID & x.EndDate == null);
                if (log != null)
                {
                    foreach (var l in log)
                    {
                        l.EndDate = Date;
                    }
                }
                ham.CageID = null;
                ham.ExerciseAreaID = null;
                ham.CheckedInTime = null;
                ham.LastExercise = null;
            }

            foreach (var c in HDCon.Cages)
            {
                c.Hamsters.Clear();
                c.HasFemale = false;
            }

            HDCon.SaveChanges();
            await Task.CompletedTask;
        }

        private void Reset()
        {

            HDCon.ActivityLogs.RemoveRange(HDCon.ActivityLogs);
            HDCon.SaveChanges();

            foreach (var ham in HDCon.Hamsters)
            {
                ham.CageID = null;
                ham.ExerciseAreaID = null;
                ham.CheckedInTime = null;
                ham.LastExercise = null;
            }

            foreach (var c in HDCon.Cages)
            {

                c.Hamsters.Clear();
                c.HasFemale = false;
            }

            HDCon.SaveChanges();
        }
        private string Print()
        {
            var print = new StringBuilder();


            var hamsters = HDCon.Hamsters.OrderBy(x => x.CageID);

            print.Append($"{"CageID",-3}{"ExerID",-3}{"Name",-15}\t{"Age",-10}\t{"Sex",-10}\t{"Owner",-30}   \t\t{"CheckedIn",-40}\t{"Exersiced",-40}" + Environment.NewLine + Environment.NewLine);

            foreach (var h in hamsters)
            {
                string female = "Female";
                string cageID = h.CageID.ToString();
                string ExID = h.ExerciseAreaID.ToString(); ;
                if (!h.IsFemale)
                    female = "Male";
                if (h.CageID == null)
                    cageID = "";
                if (h.ExerciseAreaID == null)
                    ExID = "";

                print.Append($"{cageID,-3}{ExID,-3}{h.Name,-15}\t{h.Age,-10}\t{female,-20}\t{h.Ownername,-25}   \t\t{h.CheckedInTime,-40}\t{h.LastExercise,-40}" + Environment.NewLine);
                //print.Append($"{"",-4}{cageID}{ExID,5}{h.Name,30}{h.Age,30}{female,30}{h.Ownername,50}{h.CheckedInTime,50}{h.LastExercise,50}" + Environment.NewLine);
            }

            PrintEvent?.Invoke(this, new PrintEventArgs(print.ToString(), Date));
            return print.ToString();
        }

        public string[] ShowPreviousResults()
        {

            if (!Directory.Exists(@"..\..\..\..\Logs"))
            {
                throw new Exception("The path does not exist, please run the simulation once to create it...");
            }

            string[] documents = System.IO.Directory.GetFiles("../../../../Logs/");


            for (int i = 0; i < documents.Length; i++)
            {
                documents[i] = documents[i].Replace("../../../../Logs/", "");
            }


            return documents;
        }

    }
}

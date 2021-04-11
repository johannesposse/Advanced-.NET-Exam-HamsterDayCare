using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ReportEventArgs : EventArgs
    {
        public string Data { get; set; }
        internal List<Hamster> Hamsters { get; set; }
        internal List<ActivityLog> ActivityLogs { get; set; }
        private DateTime Date { get; set; }

        internal ReportEventArgs(List<Hamster> hamsters, List<ActivityLog> activityLogs) //tar emot en instans av hamsters och activitylog
        {
            this.Hamsters = hamsters;
            this.ActivityLogs = activityLogs;
            this.Date = DateTime.Now; //sätter datumet till nuvarande datum
            GenerateRapport(); //anropar metod för att skapa ny rapport
        }
        public ReportEventArgs(string data)
        {
            this.Data = data;
        }

        public ReportEventArgs()
        {

        }

        internal void GenerateRapport() //metod för att generera och spara rapporter
        {
            string path = @"..\..\..\..\Logs\" + Date.ToString("yy-MM-dd hh-mm-ss") + ".txt"; //sökvägen dit de unika rapporterana ska genereras
            string pathTotal = @"..\..\..\..\Logs\total.txt"; //sökväg dit rapport för alla simulationer tillsammans genereras

            string root = @"..\..\..\..\Logs"; //foldern dit rapporterna ska sparas

            if (!Directory.Exists(root)) //kollar om foldern finns
            {
                Directory.CreateDirectory(root); //gör den inte det så skapas den
            }

            //slår ihop activitylogs med hamster med hjälp av hamserID och skapar ett nytt anonymt object
            var ham = ActivityLogs.Join(Hamsters, ac => ac.HamsterID, ham => ham.ID, (ac, ham) => new
            {
                Owner = ham.Ownername,
                ID = ham.ID,
                HamName = ham.Name,
                Activity = ac.ActivityName,
                Start = ac.StartDate,
                End = ac.EndDate,
            });


            foreach (var h in ham) //går igenom varje inlägg och sparar det till en unikfil för simulationen och till en fil för alla simulationer
            {
                var temp = $"{h.ID},{h.Owner},{h.HamName}, {h.Activity}, {h.Start}, {h.End}\n";
                //DirectoryInfo di = Directory.CreateDirectory(path);
                File.AppendAllText(path, temp);
                File.AppendAllText(pathTotal, temp);
            }

            PrintReports(path); //anropa print metod

        }

        public void PrintReports(string path) //metod för att skriva ut den senaste rapporten till skärm
        {
            var print = new StringBuilder();

            List<string> input = File.ReadAllLines(path).ToList(); //läsa in rapportfilen till en lista av string
            var reports = new List<Report>(); //skapar ett ny lista av report

            foreach (var i in input)
            {
                var data = i.Split(","); //skapar en temp array av från varje rad i listan som splittas på ","

                reports.Add(new Report(int.Parse(data[0]), data[1], data[2], data[3], DateTime.Parse(data[4]), DateTime.Parse(data[5]))); //lägger till datan i reportlistan
            }

            var report = reports.GroupBy(x => x.Owner).OrderBy(x => x.Key); //grupperar reportslistan på ägare



            foreach (var rep in report) //för varje report i reportlistan
            {
                print.Append(rep.Key + "------------------------------------------------------------" + Environment.NewLine); //skriver ut ägarnamnet

                foreach (var r in rep.OrderBy(x => x.Name)) //sorterar hamstarna på namn
                {
                    print.Append($"{r.Name,-20}{r.Acticity,-55}{r.Start,-30}{r.End,-30}" + Environment.NewLine); //skriver ut information om hamstern
                }
                print.Append("" + Environment.NewLine); //lägger till en ny rad
            }

            this.Data = print.ToString(); // retunerar stringbuildern som en string
        }


    }
}

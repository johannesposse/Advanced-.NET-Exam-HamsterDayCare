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

        internal ReportEventArgs(List<Hamster> hamsters, List<ActivityLog> activityLogs)
        {
            this.Hamsters = hamsters;
            this.ActivityLogs = activityLogs;
            this.Date = DateTime.Now;
            GenerateRapport();
        }
        public ReportEventArgs(string data)
        {
            this.Data = data;
        }

        internal void GenerateRapport()
        {
            string path = @"..\..\..\..\Logs\" + Date.ToString("yy-MM-dd hh-mm-ss");
            var ham = ActivityLogs.Join(Hamsters, ac => ac.HamsterID, ham => ham.ID, (ac, ham) => new
            {
                Owner = ham.Ownername,
                ID = ham.ID,
                HamName = ham.Name,
                Activity = ac.ActivityName,
                Start = ac.StartDate,
                End = ac.EndDate,
            });

            //foreach (var h in ham)
            //{
            //    var temp = $"{h.ID},{h.Owner},{h.HamName}, {h.Activity}, {h.Start}, {h.End}\n";
            //    DirectoryInfo di = Directory.CreateDirectory(path);
            //    File.AppendAllText(path +"\\"+ h.HamName + ".txt",temp);
            //}

            foreach(var h in ham)
            {
                var temp = $"{h.ID},{h.Owner},{h.HamName}, {h.Activity}, {h.Start}, {h.End}\n";
                DirectoryInfo di = Directory.CreateDirectory(path);
                File.AppendAllText(path + "\\total.txt", temp);
            }

            PrintReports(path);
        }

        private void PrintReports(string path)
        {
            var print = new StringBuilder();

            List<string> input = File.ReadAllLines(path + "\\total.txt").ToList();
            var reports = new List<Report>();

            foreach (var i in input)
            {
                var data = i.Split(",");

                reports.Add(new Report(int.Parse(data[0]), data[1], data[2], data[3], DateTime.Parse(data[4]), DateTime.Parse(data[5])));
            }

            var report = reports.GroupBy(x => x.Owner).OrderBy(x => x.Key);

            foreach (var rep in report)
            {
                print.Append(rep.Key + "------------------------------------------------------------\n");

                foreach (var r in rep.OrderBy(x => x.Name))
                {
                    print.Append($"{r.Name,-15}{r.Acticity,-25}{r.Start,-30}{r.End,-30}\n");
                }
                print.Append("\n");
            }

            this.Data = print.ToString();
        }


    }
}

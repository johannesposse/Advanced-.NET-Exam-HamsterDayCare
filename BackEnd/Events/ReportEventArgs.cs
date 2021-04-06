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

        internal ReportEventArgs(List<Hamster> hamsters, List<ActivityLog> activityLogs)
        {
            this.Hamsters = hamsters;
            this.ActivityLogs = activityLogs;
            GenerateRapport();
        }
        public ReportEventArgs(string data)
        {
            this.Data = data;
        }

        internal void GenerateRapport()
        {

            var ham = ActivityLogs.Join(Hamsters, ac => ac.HamsterID, ham => ham.ID, (ac, ham) => new
            {
                HamName = ham.Name,
                Activity = ac.ActivityName,
                Start = ac.StartDate,
                End = ac.EndDate,
            });

            foreach (var h in ham)
            {
                var temp = $"{h.HamName}, {h.Activity}, {h.Start}, {h.End}\n";
                File.AppendAllText(@"..\..\..\..\Logs\" + h.HamName + ".txt",temp);
            }

            
        }


    }
}

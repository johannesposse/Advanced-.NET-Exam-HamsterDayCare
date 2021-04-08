using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ActivityLog
    {
        public int ActivityLogID { get; set; }
        public string ActivityName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public virtual int HamsterID { get; set; }
        public virtual Hamster Hamster { get; set; }


        public ActivityLog(string activityName, DateTime startDate, int hamsterID)
        {
            this.ActivityName = activityName;
            this.StartDate = startDate;
            this.HamsterID = hamsterID;
        }
    }
}

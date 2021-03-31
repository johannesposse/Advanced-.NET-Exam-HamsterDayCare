using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class TickEventArgs : EventArgs
    {
        public DateTime Date { get; set; }
        public bool isPaused { get; set; }
        public DateTime EndDate { get; set; }

        public TickEventArgs(DateTime startDate, DateTime endDate, bool isPaused = false)
        {
            this.Date = startDate;
            this.EndDate = endDate;
            this.isPaused = isPaused;
        }

        public override string ToString()
        {
            return this.Date.ToString("YY:MM:DD hh:mm:ss");
        }
    }


}

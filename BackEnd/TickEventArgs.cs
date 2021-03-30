using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    internal class TickEventArgs : EventArgs
    {
        internal DateTime Date { get; set; }
        internal bool isPaused { get; set; }
        internal DateTime EndDate { get; set; }

        internal TickEventArgs(DateTime startDate, DateTime endDate, bool isPaused = false)
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

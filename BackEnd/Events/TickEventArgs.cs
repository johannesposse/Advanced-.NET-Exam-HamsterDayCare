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
        public bool IsPaused { get; set; }
        public DateTime EndDate { get; set; }

        public TickEventArgs(DateTime startDate, DateTime endDate, bool isPaused = false) //tar emot startdatum, slutdatum, pausbool som default sätts till false
        {
            this.Date = startDate;
            this.EndDate = endDate;
            this.IsPaused = isPaused;
        }

        public override string ToString()
        {
            return this.Date.ToString("YY:MM:DD hh:mm:ss"); //retunerar datumet som en string
        }
    }


}

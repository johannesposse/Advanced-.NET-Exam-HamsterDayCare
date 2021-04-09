using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    internal class Ticker
    {
        internal event EventHandler<TickEventArgs> Tick;

        internal void StartTick(int ticksPerSecond, int days)
        {
            var startDate = DateTime.Parse("1993 - 08 - 01 07:00:00");
            var endDate = startDate.AddDays(days);
            var tickEventArgs = new TickEventArgs(startDate,endDate); 

            while (!tickEventArgs.IsPaused & tickEventArgs.Date <= tickEventArgs.EndDate)
            {
                Tick?.Invoke(this, tickEventArgs);
                System.Threading.Thread.Sleep(ticksPerSecond);
                tickEventArgs.Date = tickEventArgs.Date.AddMinutes(6);
            }
        } 
    }
}

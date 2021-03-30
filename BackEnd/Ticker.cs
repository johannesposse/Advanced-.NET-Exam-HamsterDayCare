using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    internal class Ticker
    {
        internal event EventHandler<TickEventArgs> tick;
        

        internal void StartTick(int ticksPerSecond, int days)
        {
            DateTime startDate = DateTime.Parse("1993 - 08 - 01 06:30:00");
            DateTime endDate = startDate.AddDays(days);
            TickEventArgs tickEventArgs = new TickEventArgs(startDate,endDate); 

            while (!tickEventArgs.isPaused & tickEventArgs.Date != tickEventArgs.EndDate)
            {
                tick?.Invoke(this, tickEventArgs);
                System.Threading.Thread.Sleep(ticksPerSecond);
                tickEventArgs.Date = tickEventArgs.Date.AddMinutes(6);
            }
        }

        
    }
}

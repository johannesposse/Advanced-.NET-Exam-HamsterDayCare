using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    internal class Ticker
    {
        internal event EventHandler<TickEventArgs> Tick; //event som driver hela simuleringen

        internal void StartTick(int ticksPerSecond, int days) //tar emot hastighet och hur länge simuleringen ska ske
        {
            var startDate = DateTime.Parse("1993 - 08 - 01 07:00:00"); //sätter startdatum
            var endDate = startDate.AddDays(days); //sätter slutdatumet med hjälp av startdatum och antal dagar som ska läggas till
            var tickEventArgs = new TickEventArgs(startDate,endDate);  //gör en ny instans av tickeventargs men startdatum och slutdatum

            while (!tickEventArgs.IsPaused & tickEventArgs.Date < tickEventArgs.EndDate) //medans pausboolen är falsk och startdatumet är mindre än slutdatumet körs loopen
            {
                Tick?.Invoke(this, tickEventArgs); //invokar eventet med instansen av tickeventargs
                System.Threading.Thread.Sleep(ticksPerSecond); //pausar tråden i den bestämda hamstigheten
                tickEventArgs.Date = tickEventArgs.Date.AddMinutes(6); //lägger till 6 minuter på simulationsdatumet för varje tick
            }
        } 
    }
}

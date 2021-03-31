using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class PrintEventArgs : EventArgs
    {
        public string Data { get; set; }
        public DateTime Date { get; set; }

        public PrintEventArgs(string data, DateTime date)
        {
            this.Data = data;
            this.Date = date;
        }

        public override string ToString()
        {
            return this.Data;
        }
    }
}

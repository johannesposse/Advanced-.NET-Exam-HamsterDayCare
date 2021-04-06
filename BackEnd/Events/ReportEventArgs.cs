using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ReportEventArgs : EventArgs
    {
        public string Data { get; set; }

        public ReportEventArgs(string data)
        {
            this.Data = data;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    internal class Report
    {
        internal int ID { get; set; }
        internal string Owner { get; set; }
        internal string Name { get; set; }
        internal string Acticity { get; set; }
        internal DateTime Start { get; set; }
        internal DateTime End { get; set; }

        internal Report(int id, string owner, string name, string activity, DateTime start, DateTime end)
        {
            ID = id;
            Owner = owner;
            Name = name;
            Acticity = activity;
            Start = start;
            End = end;
        }
    }
}

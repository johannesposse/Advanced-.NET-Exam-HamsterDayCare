using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Report //hjälp class för att enklare generera text rapporter
    {
        public int ID { get; set; }
        public string Owner { get; set; }
        public string Name { get; set; }
        public string Acticity { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public Report(int id, string owner, string name, string activity, DateTime start, DateTime end)
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

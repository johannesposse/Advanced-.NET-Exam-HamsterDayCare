using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Cage
    {
        public int ID { get; internal set; }
        public int Number { get; internal set; }
        public int MaxSize { get; internal set; } = 3;
        public bool HasFemale { get; internal set; } = false;
        public virtual IList<Hamster> Hamsters { get; internal set; }

        public Cage()
        {
            
        }

        public Cage(int number)
        {
            this.MaxSize = 3;
            this.Number = number;
        }
    }

    

}

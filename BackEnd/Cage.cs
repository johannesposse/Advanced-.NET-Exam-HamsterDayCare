using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Cage
    {
        public int ID { get; private set; }
        public int Number { get; private set; }
        public int MaxSize { get; private set; }
        public int Size { get; set; }
        public virtual IList<Hamster> Hamsters { get; private set; }

        public Cage()
        {
            
        }

        public Cage(int number)
        {
            this.MaxSize = 3;
            this.Number = number;
            this.Size = 0;
        }

        public void AddHamster(Hamster hamster)
        {
            
        }

        public void RemoveHamster(Hamster hamster)
        {

        }
    }

    

}

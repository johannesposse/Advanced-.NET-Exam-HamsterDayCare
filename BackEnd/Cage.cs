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
        public int MaxSize { get; private set; } = 3;
        public int Size { get; private set; }
        public virtual IList<Hamster> Hamsters { get; private set; }

        public Cage()
        {

        }

        public void AddHamster(Hamster hamster)
        {
            
        }

        public void RemoveHamster(Hamster hamster)
        {

        }
    }

    

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Cage
    {
        public int ID { get; set; }
        public int Number { get; set; }
        public int MaxSize { get; set; } = 3;
        public int Size { get; set; }
        public virtual IList<Hamster> Hamsters { get; set; }

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

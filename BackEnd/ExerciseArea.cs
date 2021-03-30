using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ExerciseArea
    {
        public int ID { get; private set; }
        public int MaxSize { get; private set; } = 6;
        public int Size { get; private set; }
        public virtual IList<Hamster> Hamsters { get; private set; }

        public ExerciseArea()
        {
            this.Size = 0;
            this.MaxSize = 6;
        }

        public void AddHamster(Hamster hamster)
        {

        }

        public void RemoveHamster(Hamster hamster)
        {

        }
    }
}

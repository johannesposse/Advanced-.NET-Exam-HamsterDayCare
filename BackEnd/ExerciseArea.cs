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
        public int MaxSlots { get; private set; } = 6;
        public virtual IList<Hamster> Hamsters { get; private set; }

        public ExerciseArea()
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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ExerciseArea
    {
        public int ID { get; internal set; }
        public int MaxSize { get; internal set; } = 6;
        public virtual IList<Hamster> Hamsters { get; internal set; }

        public ExerciseArea()
        {
            this.MaxSize = 6;
        }
    }
}

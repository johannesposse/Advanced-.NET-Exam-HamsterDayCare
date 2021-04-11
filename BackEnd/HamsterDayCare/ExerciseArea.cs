using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class ExerciseArea //klass för att hålla införmation för träningsområdet
    {
        public int ID { get; internal set; }
        public int MaxSize { get; internal set; } = 6; //sätts som default till 6
        public bool HasFemale { get; set; } = false; //bool för att hålla koll på om träningsområdet har hanar eller honom
        public virtual IList<Hamster> Hamsters { get; internal set; } //lista med de hamstar som tränar för tillfället

        public ExerciseArea()
        {
            this.MaxSize = 6; //sätts som default till 6
        }
    }
}

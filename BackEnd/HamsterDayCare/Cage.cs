using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Cage //klass för att hålla införmation för burar
    {
        public int ID { get; internal set; }
        public int Number { get; internal set; }
        public int MaxSize { get; internal set; } = 3; //sätts som default till 3
        public bool HasFemale { get; internal set; } = false; //bool för att hålla koll på om buren har hanar eller honom
        public virtual IList<Hamster> Hamsters { get; internal set; } //lista med de hamstrar buren ahr

        public Cage()
        {
            
        }

        public Cage(int number) //konstruktor för att skapa en ny bur
        {
            this.MaxSize = 3; //sätts som default till 3
            this.Number = number;
        }
    }

    

}

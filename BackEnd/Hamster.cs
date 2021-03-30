using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Hamster
    {
        public int ID { get; private set; }
        public string Name { get; private set; }
        public string Ownername { get; private set; }
        public int Age { get; private set; }
        public bool IsFemale { get; private set; }
        public DateTime? CheckedInTime {get; private set; }
        public DateTime? LastExercise { get; private set; }

        public int CageID { get; private set; }
        public virtual Cage Cage { get; private set; }

        public int ExerciseAreaID { get; private set; }
        public virtual ExerciseArea ExerciseArea { get; private set; }

        public Hamster()
        {

        }

        public Hamster(string name, string ownerName, int age, bool isFemale, DateTime checkedInTime, DateTime lastExercise)
        {
            this.Name = name;
            this.Ownername = ownerName;
            this.Age = age;
            this.IsFemale = isFemale;
            this.CheckedInTime = checkedInTime;
            this.LastExercise = lastExercise;
        }
    }
}

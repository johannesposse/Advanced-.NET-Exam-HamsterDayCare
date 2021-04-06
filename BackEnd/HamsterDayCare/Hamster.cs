using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Hamster
    {
        public int ID { get; internal set; }
        public string Name { get; internal set; }
        public string Ownername { get; internal set; }
        public decimal Age { get; internal set; }
        public bool IsFemale { get; internal set; }
        public DateTime? CheckedInTime {get; internal set; }
        public DateTime? LastExercise { get; set; }

        public int? CageID { get; internal set; }
        public virtual Cage Cage { get; internal set; }

        public int? ExerciseAreaID { get; internal set; }
        public virtual ExerciseArea ExerciseArea { get; internal set; }

        public Hamster()
        {

        }

        public Hamster(string name, string ownerName, decimal age, bool isFemale) //DateTime checkedInTime, DateTime lastExercise
        {
            this.Name = name;
            this.Ownername = ownerName;
            this.Age = age;
            this.IsFemale = isFemale;
            //this.CheckedInTime = checkedInTime;
            //this.LastExercise = lastExercise;
        }

        
    }
}

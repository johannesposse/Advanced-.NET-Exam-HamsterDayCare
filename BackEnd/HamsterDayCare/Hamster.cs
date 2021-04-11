﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class Hamster //klass för att hålla införmation för hamstrar
    {
        public int ID { get; internal set; }
        public string Name { get; internal set; }
        public string Ownername { get; internal set; }
        public decimal Age { get; internal set; }
        public bool IsFemale { get; internal set; }
        public DateTime? CheckedInTime {get; internal set; } //nullbar, är den null är hamstern hemma
        public DateTime? LastExercise { get; set; } //nullbar, är den null så har hamster inte tränat ännu

        public int? CageID { get; internal set; } //nullbar, för att den inte alltid måste ha en bur
        public virtual Cage Cage { get; internal set; }

        public int? ExerciseAreaID { get; internal set; } //nullbar, för att den inte alltid tränar
        public virtual ExerciseArea ExerciseArea { get; internal set; }

        public Hamster()
        {

        }

        public Hamster(string name, string ownerName, decimal age, bool isFemale)
        {
            this.Name = name;
            this.Ownername = ownerName;
            this.Age = age;
            this.IsFemale = isFemale;
        }

        
    }
}

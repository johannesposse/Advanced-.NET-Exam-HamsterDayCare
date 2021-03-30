using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class HamsterDayCareContext : DbContext
    {
        //private static event EventHandler<TickEventArgs> Tick; denna ska vara loggen
        private Ticker ticker = new Ticker();

        public virtual DbSet<Hamster> Hamsters { get; set; }
        public virtual DbSet<Cage> Cages { get; set; }
        public virtual DbSet<ExerciseArea> ExerciseArea { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder dBContextbuilder)
        {
            dBContextbuilder.UseSqlServer(@"Server=.\SQLExpress;Database=advJohannesPosse;Trusted_Connection=True;").UseLazyLoadingProxies();
        }

        public bool InitilizeDatabase(out bool dbHasData)
        {
            dbHasData = true;

            if(Cages.Count() < 1)
            {
               
                for (int i = 0; i < 10; i++)
                {
                    var tempCage = new Cage(i);
                    Cages.Add(tempCage);
                }
            }

            if(ExerciseArea.Count() < 1)
            {
                var tempExerciseArea = new ExerciseArea();
                ExerciseArea.Add(tempExerciseArea);
            }

            if(Hamsters.Count() < 1)
            {
                List<string> hamsterData = File.ReadAllLines(@"..\..\..\..\Hamsterlista30.csv").ToList();

                for (int i = 0; i < hamsterData.Count; i++)
                {
                    string[] data = hamsterData[i].Split(";");
                    bool isFemale = true;
                    if(data[2] == "M")
                    {
                        isFemale = false;
                    }
                    var tempHamster = new Hamster(data[0], data[3], int.Parse(data[1]), isFemale);
                    Hamsters.Add(tempHamster);

                }
            }

            if (Cages.Count() < 1 ^ ExerciseArea.Count() < 1 ^ Hamsters.Count() < 1)
            {
                dbHasData = false;
            }

            SaveChanges();
            return dbHasData;
        }

        public void StartSimulation(int days, int ticksPerSecond)
        {
            ticksPerSecond = 1000 / ticksPerSecond;
            ticker.tick += StartThreads;
            ticker.StartTick(ticksPerSecond, days);
        }

       

        private void StartThreads(object sender, TickEventArgs e)
        {
            Console.SetCursorPosition(5, 40);
            Console.WriteLine(e.Date);
        }

        private async Task Tasks()
        {
            var addHamstersToCages = new Task(AddHamstersToCages);


            addHamstersToCages.Start();

            await Task.WhenAll(addHamstersToCages);
        }
        private  void AddHamstersToCages()
        {
            var maleHamsters = Hamsters.Where(x => x.IsFemale == false);
            var femaleHamsters = Hamsters.Where(x => x.IsFemale == true);

 
        }
      




        public string Print()
        {
            var print = new StringBuilder();

            var hamsters = Hamsters;

            print.Append($"{"Name",-15}{"Age",-10}{"Kön",-10}{"Owner",-20}\n");
            print.Append("----------------------------------------------------------------------------\n\n");
            foreach (var hamster in hamsters)
            {
                string female = "Female";
                if (hamster.IsFemale == false)
                    female = "Male";

                print.Append($"{hamster.Name,-15}{hamster.Age,-10}{female,-10}{hamster.Ownername,-20}\n");
            }

            return print.ToString();
        }
    }
}
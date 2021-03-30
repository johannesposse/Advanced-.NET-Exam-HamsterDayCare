using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd
{
    public class HamsterDayCare
    {
        private Ticker ticker = new Ticker();

        private HamsterDayCareContext HDCon = new HamsterDayCareContext();

        private int counter = 0;

        public bool InitilizeDatabase(out bool dbHasData)
        {
            dbHasData = true;


            if (HDCon.Cages.Count() < 1)
            {

                for (int i = 0; i < 10; i++)
                {
                    var tempCage = new Cage(i);
                    HDCon.Cages.Add(tempCage);
                }
            }

            if (HDCon.ExerciseArea.Count() < 1)
            {
                var tempExerciseArea = new ExerciseArea();
                HDCon.ExerciseArea.Add(tempExerciseArea);
            }

            if (HDCon.Hamsters.Count() < 1)
            {
                List<string> hamsterData = File.ReadAllLines(@"..\..\..\..\Hamsterlista30.csv").ToList();

                for (int i = 0; i < hamsterData.Count; i++)
                {
                    string[] data = hamsterData[i].Split(";");
                    bool isFemale = true;
                    if (data[2] == "M")
                    {
                        isFemale = false;
                    }
                    var tempHamster = new Hamster(data[0], data[3], int.Parse(data[1]), isFemale);
                    HDCon.Hamsters.Add(tempHamster);

                }
            }

            if (HDCon.Cages.Count() < 1 ^ HDCon.ExerciseArea.Count() < 1 ^ HDCon.Hamsters.Count() < 1)
            {
                dbHasData = false;
            }

            HDCon.SaveChanges();


            return dbHasData;
        }

        public void StartSimulation(int days, int ticksPerSecond)
        {
            ticksPerSecond = 1000 / ticksPerSecond;
            //ticker.tick += StartThreads;
            //ticker.StartTick(ticksPerSecond, days);

            AddHamstersToCages();
        }

        private void StartThreads(object sender, TickEventArgs e)
        {
            //Tasks();

            Console.SetCursorPosition(50, 10);
            Console.WriteLine(e.Date);

            var a = StartTasks();
            

        }

        private async Task StartTasks()
        {
            counter++;
            var Add = new Task(AddHamstersToCages);
            Console.WriteLine(counter);
            Add.Start();
            await Task.WhenAll(Add);
        }

        private void AddHamstersToCages()
        {
            var hamsters = HDCon.Hamsters;
            var cages = HDCon.Cages;

            foreach(var hamster in hamsters)
            {
                var cage = cages.First(x => x.Size < 3);
                 cage.Hamsters.Add(hamster);
                //hamster.CageID = cage.ID;
                //var cage = cages.Where(x => x.ID == id).FirstOrDefault();
                cage.Size++;
            }
            HDCon.SaveChanges();
            Console.WriteLine("hejsan");
            
        }


        

        public string Print()
        {
            var print = new StringBuilder();

            var hamsters = HDCon.Hamsters;

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

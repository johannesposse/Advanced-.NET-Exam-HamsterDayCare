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

        private static HamsterDayCareContext HDCon = new HamsterDayCareContext();

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
            ticker.tick += StartThreads;
            ticker.StartTick(ticksPerSecond, days);

            //AddHamstersToCages();
            //reset();
        }

        private void StartThreads(object sender, TickEventArgs e)
        {
            TimeSpan start = new TimeSpan(07, 0, 0);
            TimeSpan end = new TimeSpan(17, 0, 0);
            bool openHours = TimeBetween(e.Date, start, end);
            if (openHours)
            {
                
            }
            else
            {
                
            }

            Console.SetCursorPosition(50, 6);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Date: " + e.Date);
            Console.ForegroundColor = ConsoleColor.White;
            //var a = StartTasks();


        }

        bool TimeBetween(DateTime datetime, TimeSpan start, TimeSpan end)
        {
            TimeSpan now = datetime.TimeOfDay;
            if (start < end)
                return start <= now && now <= end;

            return !(end < now && now < start);
        }

        private async Task StartTasks()
        {
            counter++;
            var Add = new Task(AddHamstersToCages);
            Console.WriteLine(counter);
            Add.Start();
            await Task.WhenAll(Add);
        }

        public void AddHamstersToCages()
        {
            var hamsters = HDCon.Hamsters.OrderByDescending(x => x.IsFemale).ToList();
            var cages = HDCon.Cages;

            for (int i = 0; i < hamsters.Count(); i++)
            {
                if(hamsters[i].ExerciseAreaID == null)
                {
                    var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1)));

                    if (cage != null)
                    {
                        cage.Hamsters.Add(hamsters[i]);
                        cage.HasFemale = hamsters[i].IsFemale;
                        HDCon.SaveChanges();
                    }
                }
            }
        }

        public void reset()
        {
            foreach (var ham in HDCon.Hamsters)
            {
                ham.CageID = null;
            }

            foreach (var c in HDCon.Cages)
            {
                c.Hamsters.Clear();
                c.HasFemale = false;
            }

            HDCon.SaveChanges();
        }

        public string Print()
        {
            var print = new StringBuilder();

            var hamsters = HDCon.Hamsters.AsEnumerable().OrderBy(x => x.CageID).GroupBy(x => x.CageID);

            foreach (var cage in hamsters)
            {
                print.Append("\nCage: " + cage.Key + "\n-------------------------------\n");
                foreach (var hamster in cage)
                {
                    string female = "Female";
                    if (!hamster.IsFemale)
                          female = "Male";
                        print.Append($"{hamster.Name,-15}{hamster.Age,-10}{female,-10}{hamster.Ownername,-20}\n");
                }
            }

            //print.Append($"{"Name",-15}{"Age",-10}{"Kön",-10}{"Owner",-20}\n");
            //print.Append("----------------------------------------------------------------------------\n\n");
            //foreach (var hamster in hamsters)
            //{
            //    string female = "Female";
            //    if (!hamster.IsFemale)
            //        female = "Male";

            //    print.Append($"{hamster.Name,-15}{hamster.Age,-10}{female,-10}{hamster.Ownername,-20}\n");
            //}

            return print.ToString();
        }
    }
}

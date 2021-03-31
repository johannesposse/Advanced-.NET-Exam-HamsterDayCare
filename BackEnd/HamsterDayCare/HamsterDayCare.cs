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
        public event EventHandler<PrintEventArgs> PrintEvent;
        private static HamsterDayCareContext HDCon = new HamsterDayCareContext();
        private DateTime Date;

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
        }

        private void StartThreads(object sender, TickEventArgs e)
        {
            
            
            if (e.Date.Hour == 17 & e.Date.Minute == 0)
            {
                Console.Clear();
                CheckOutHamstersForTheDay();
                //e.isPaused = true;
            }else if (e.Date.Hour == 6 & e.Date.Minute == 56)
            {
                Console.Clear();
            }
            else if (e.Date.Hour >= 17 | e.Date.Hour < 7)
            {
                PrintEvent?.Invoke(this, new PrintEventArgs(Print(), e.Date));
                Console.WriteLine(e.Date);
            }
            else if(e.Date.Hour >= 7 & e.Date.Hour <= 17)
            {
                Date = e.Date;
                PrintEvent?.Invoke(this, new PrintEventArgs(Print(),e.Date));
                AddHamstersToCages();
            }

            
        }

        private void AddHamstersToExerciseArea()
        {
            throw new NotImplementedException();
        }

        public void AddHamstersToCages()
        {
            var hamsters = HDCon.Hamsters.OrderByDescending(x => x.IsFemale).ToList();
            var cages = HDCon.Cages;

            for (int i = 0; i < hamsters.Count(); i++)
            {
                if(hamsters[i].ExerciseAreaID == null & hamsters[i].CageID == null)
                {
                    var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1)));

                    if (cage != null)
                    {
                        cage.Hamsters.Add(hamsters[i]);
                        cage.HasFemale = hamsters[i].IsFemale;

                        if(hamsters[i].CheckedInTime == null)
                        {
                            hamsters[i].CheckedInTime = Date;
                        }
                        HDCon.SaveChanges();
                    }
                }
            }
        }

        public void CheckOutHamstersForTheDay()
        {
            foreach (var ham in HDCon.Hamsters)
            {
                ham.CageID = null;
                ham.CheckedInTime = null;
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
                if(cage.Key != null)
                    print.Append("\nCage: " + cage.Key + "\n--------------------------------------------------------------\n");
                else
                    print.Append("\nNot in cage: " + "\n--------------------------------------------------------------\n");
                foreach (var hamster in cage)
                {
                    string female = "Female";
                    if (!hamster.IsFemale)
                          female = "Male";
                        print.Append($"{hamster.Name,-15}{hamster.Age,-10}{female,-10}{hamster.Ownername,-25}{hamster.CheckedInTime.ToString(),-20}{hamster.LastExercise.ToString(),-20}\n");
                }
            }

            return print.ToString();
        }
    }
}

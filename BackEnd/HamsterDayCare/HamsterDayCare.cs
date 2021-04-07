using BackEnd.Extensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace BackEnd
{
    public class HamsterDayCare
    {
        private Ticker ticker = new Ticker();
        public event EventHandler<PrintEventArgs> PrintEvent;
        public event EventHandler<ReportEventArgs> ReportEvent;
        private HamsterDayCareContext HDCon = new HamsterDayCareContext();
        private DateTime Date;

        public bool InitilizeDatabase(out bool dbHasData)
        {
            dbHasData = true;


            if (!HDCon.Cages.Any())
            {
                for (int i = 0; i < 10; i++)
                {
                    var tempCage = new Cage(i);
                    HDCon.Cages.Add(tempCage);
                }
            }

            if (!HDCon.ExerciseArea.Any())
            {
                var tempExerciseArea = new ExerciseArea();
                HDCon.ExerciseArea.Add(tempExerciseArea);
            }

            if (!HDCon.Hamsters.Any())
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
                    var tempHamster = new Hamster(data[0], data[3], Math.Round(decimal.Parse(data[1]) / 12, 1), isFemale);
                    HDCon.Hamsters.Add(tempHamster);
                }
            }

            if (!HDCon.Cages.Any() ^ !HDCon.ExerciseArea.Any() ^ !HDCon.Hamsters.Any())
            {
                dbHasData = false;
            }

            HDCon.SaveChanges();

            return dbHasData;
        }

        public void StartSimulation(int days, int ticksPerSecond)
        {
            ticksPerSecond = 1000 / ticksPerSecond;
            ticker.Tick += StartThreads;
            ticker.StartTick(ticksPerSecond, days);
        }

        private async void StartThreads(object sender, TickEventArgs e)
        {

            if (e.Date.TimeOfDay == TimeSpan.Parse("17:00:00")) //e.Date.Hour == 17 & e.Date.Minute == 0
            {
                Date = e.Date;
                Console.Clear();

                var checkOutTask = CheckOutHamstersForTheDay();
                await Task.WhenAll(checkOutTask);

                e.IsPaused = true;
                ReportEvent?.Invoke(this, new ReportEventArgs(HDCon.Hamsters.ToList(), HDCon.ActivityLogs.ToList()));

                var logs = HDCon.ActivityLogs;
                HDCon.ActivityLogs.RemoveRange(logs);
                HDCon.SaveChanges();

                e.Date = e.Date.AddHours(14);
                e.IsPaused = false;
            }
            else if (e.Date.Hour >= 7 & e.Date.TimeOfDay <= TimeSpan.Parse("17:00:00"))
            {
                Date = e.Date;
                PrintEvent?.Invoke(this, new PrintEventArgs(Print(), e.Date));
                var addToCageTask = AddHamstersToCages();
                var retrieveFromExerciseTask = RetreiveHamstersFromExtersiceArea();
                var addToExerciseTask = AddHamstersToExerciseArea();

                await addToCageTask;
                await retrieveFromExerciseTask;
                await addToExerciseTask;

                //await Task.WhenAll(addToCageTask, retrieveFromExerciseTask, addToExerciseTask);
            }
        }

       
        private async Task AddHamstersToExerciseArea()
        {
            var hamsters = HDCon.Hamsters.Where(x => x.CageID != null).OrderBy(x => x.LastExercise).ToList();
            var exerciseArea = HDCon.ExerciseArea.First();
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++)
            {
                if (exerciseArea.Hamsters.Count < 1 | exerciseArea.Hamsters.Select(x => x.IsFemale).FirstOrDefault() == hamsters[i].IsFemale)
                {
                    if (exerciseArea.Hamsters.Count < exerciseArea.MaxSize)
                    {
                        var cage = cages.Where(x => x.Hamsters.Contains(hamsters[i])).FirstOrDefault();
                        if (cage.Hamsters.Count == 1)
                            cage.HasFemale = false;

                        var log = logs.Where(x => x.ActivityName == hamsters[i].CageID.ToString() & x.EndDate == null).FirstOrDefault();
                        log.EndDate = Date;
                        hamsters[i].LastExercise = Date;
                        hamsters[i].CageID = null;
                        exerciseArea.Hamsters.Add(hamsters[i]);
                        logs.Add(new ActivityLog("Exercise", Date, hamsters[i].ID));
                        HDCon.SaveChanges();
                    }
                    else
                    {
                        break;
                    }
                }
            }

            await Task.CompletedTask;
        }
        private async Task RetreiveHamstersFromExtersiceArea()
        {

            var exerciseArea = HDCon.ExerciseArea.First();
            var hamsters = exerciseArea.Hamsters.Where(x => x.LastExercise.Value.Hour + 1 == Date.Hour).ToList();
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++)
            {
                var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1)));

                if (cage != null)
                {
                    cage.Hamsters.Add(hamsters[i]);
                    cage.HasFemale = hamsters[i].IsFemale;
                    hamsters[i].ExerciseAreaID = null;
                    var log = logs.Where(x => x.HamsterID == hamsters[i].ID & x.ActivityName == "Exercise" & x.EndDate == null).FirstOrDefault();
                    log.EndDate = Date;
                    logs.Add(new ActivityLog(cage.ID.ToString(), Date, hamsters[i].ID));
                    HDCon.SaveChanges();
                }
            }

            await Task.CompletedTask;
        }
        private async Task AddHamstersToCages()
        {

            //var hamsters = HDCon.Hamsters.OrderBy(x => x.IsFemale).ToList();
            var hamsters = HDCon.Hamsters.Shuffle().ToList();
            var cages = HDCon.Cages;
            var logs = HDCon.ActivityLogs;

            for (int i = 0; i < hamsters.Count; i++)
            {
                if (hamsters[i].ExerciseAreaID == null & hamsters[i].CageID == null)
                {
                    var cage = cages.AsEnumerable().FirstOrDefault(x => x.Hamsters.Count < x.MaxSize & ((x.HasFemale == hamsters[i].IsFemale) | (x.Hamsters.Count < 1)));

                    if (cage != null)
                    {
                        cage.Hamsters.Add(hamsters[i]);
                        cage.HasFemale = hamsters[i].IsFemale;
                        logs.Add(new ActivityLog("Checked In for The Day", Date, hamsters[i].ID));
                        logs.Add(new ActivityLog(cage.ID.ToString(), Date, hamsters[i].ID));

                        if (hamsters[i].CheckedInTime == null)
                        {
                            hamsters[i].CheckedInTime = Date;
                        }
                        HDCon.SaveChanges();
                    }
                }
            }


            await Task.CompletedTask;
        }
        public async Task CheckOutHamstersForTheDay()
        {
            var logs = HDCon.ActivityLogs;

            foreach (var ham in HDCon.Hamsters)
            {
                //var log = logs.Where(x => x.HamsterID == ham.ID & x.ActivityName == "Checked In for The Day" & x.EndDate == null).FirstOrDefault();
                var log = logs.Where(x => x.HamsterID == ham.ID & x.EndDate == null);
                if (log != null)
                {
                    foreach (var l in log)
                    {
                        l.EndDate = Date;
                    }
                }
                ham.CageID = null;
                ham.ExerciseAreaID = null;
                ham.CheckedInTime = null;
                ham.LastExercise = null;
            }

            foreach (var c in HDCon.Cages)
            {
                c.Hamsters.Clear();
                c.HasFemale = false;
            }

            HDCon.SaveChanges();
            await Task.CompletedTask;
        }

        public string Print()
        {
            var print = new StringBuilder();


            var hamsters = HDCon.Hamsters.OrderBy(x => x.CageID);

            print.Append($"\n\n{"",-4}{"CageID",-7}{"ExerID",-7}{"Name",-15}{"Age",-10}{"Kön",-10}{"Owner",-25}{"CheckedIn",-25}{"Exersiced",-25}\n\n");

            foreach (var h in hamsters)
            {
                string female = "Female";
                string cageID = h.CageID.ToString();
                string ExID = h.ExerciseAreaID.ToString(); ;
                if (!h.IsFemale)
                    female = "Male";
                if (h.CageID == null)
                    cageID = "";
                if (h.ExerciseAreaID == null)
                    ExID = "";
                print.Append($"{"",-4}{cageID,-7}{ExID,-7}{h.Name,-15}{h.Age,-10}{female,-10}{h.Ownername,-25}{h.CheckedInTime,-25}{h.LastExercise,-25}\n");
            }
            return print.ToString();
        }

        

    }
}

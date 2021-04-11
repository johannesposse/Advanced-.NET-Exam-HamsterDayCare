using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEnd_Forms
{
    public partial class FrmStatistics : Form
    {
        static BackEnd.HamsterDayCare hamsterDayCare = new BackEnd.HamsterDayCare();

        public FrmStatistics()
        {
            InitializeComponent();
        }

        private void MostPopularCage(List<BackEnd.Report> reports) //metod för att hitta den mest populärar buren
        {
            //grupperar på activiteter och räknar hur många av varje aktivitet det finns, sorterar dom på den aktivitet som hänt mest
            var d = reports.GroupBy(x => x.Acticity).OrderByDescending(id => id.Count()).Select(g => new { Id = g.Key, Count = g.Count() });
            //hämtar ut den första aktiviteten som är en cage
            var b = d.Where(x => x.Id.Contains("Cage")).FirstOrDefault();

            //skriver ut den mest populära buren och hur många gången den besökts
            lbl_MostPopularCage.Text = b.Id.Trim();
            lbl_MostPopularCageID.Text = b.Count.ToString() + " visits";
        }

        private void MosteExercisedHamster(List<BackEnd.Report> reports) //metod för att hitta den hamstern som tränat mest
        {
            //hämtar de aktiviteter som är exersice och grupperar på hamstrarnas namn
            var hamsters = reports.Where(x => x.Acticity == " Exercise").GroupBy(x => x.Name).Select(x => new { Name = x.Key, Count = x.Count() });

            //sorterar så att hamstern som tränat mest kommer först och hämtar ut det första inlägget
            var mostExercised = hamsters.OrderByDescending(x => x.Count).FirstOrDefault();

            //skriver ut den hamstern som tränat mest och vad den heter
            lbl_MosteExercisedTimes.Text = "Has exercised " + mostExercised.Count.ToString() + " times";
            lbl_MostExercisedName.Text = mostExercised.Name;
        }

        private void AverageWaitingTimeToExercise(List<BackEnd.Report> reports) //metod för att hitta hur länge hamstrarna fått vänta i snitt på att träna
        {
            var options = hamsterDayCare.ShowPreviousResults(); //hämtar ut hur många gånger simulationen körts

            TimeSpan start = TimeSpan.Parse("07:00:00"); //startdatum

            var exercise = reports.Where(x => x.Acticity == " Exercise").OrderBy(x => x.Start); //hämtar ut de aktiviteter som är extersice och sorterar så tidigast kommer först

            int timeDifference = 0;
            int num = 30 * (options.Length - 1); //kollar hur många simulationer som gjort, tar bort en (total.txt), gånger 30 för att plocka fram den första träningen de gjort alla dagar
                
            if (options.Any()) //kollar om det finns några simuleringar rapporterade
            {
                var firstExersice = exercise.Take(num); //hämtar ut de antalet första träningar 

                foreach (var first in firstExersice) //loopar igenom alla träningar
                {
                    var timeSpanDifference = first.Start.TimeOfDay - start; //kollar skillnaden mellan 07.00 och när träningen startade
                    timeDifference += timeSpanDifference.Hours; //lägger till tidsskillnaden till en int
                }

                double averageTime = timeDifference / num; //tar fram medelvärdet
                lbl_AverageWatingToExercise.Text = averageTime + " hours"; //skriver ut medelvärdet
            }
            else //om det inte finns några rapporter 
            {
                lbl_AverageWatingToExercise.Text = "N/A";
            }  
        }

        //private void HamsterStatestic(List<BackEnd.Report> reports)
        //{
        //    StringBuilder info = new StringBuilder();

        //    var Exersice = reports.Where(x => x.Acticity == " Exercise").GroupBy(x => x.Name).Select(x => new { Name = x.Key, Count = x.Count() });
        //    var Cage = reports.Where(x => x.Acticity.Contains(" Cage:")).GroupBy(x => x.Name);

            

        //    txt_HamsterStatic.Text = info.ToString();
        //}

        private void AverageTimesExercisedPerDay(List<BackEnd.Report> reports) //metod för att visa i snitt hur många gånger de tränar per dag
        {
            var options = hamsterDayCare.ShowPreviousResults(); //hämtar ut hur många gånger simulationen körts
            var exercise = reports.Where(x => x.Acticity == " Exercise"); //hämar ut de aktiviteter som är exercise

            if (exercise.Any()) //om det inte är tomt
            {
                var exCount = exercise.Count(); //räknar ihop hur många exercise det finns
                double times = Convert.ToDouble(exCount) / 30; //delar på 30 (finns 30 hamstrar)
                lbl_AverageTimesExercisedPerDayNum.Text = times.ToString(); //skriver ut snittvärdet
            }
            else //om det inte finns några tidigare simuleringar
            {
                lbl_AverageTimesExercisedPerDayNum.Text = "N/A";
            }
        }

        private void FrmStatistics_Load(object sender, EventArgs e)
        {

            if (File.Exists(@"..\..\..\..\Logs\total.txt")) //kollar om total.txt dokumentet finns
            {
                string path = @"..\..\..\..\Logs\total.txt"; //sökväg för total.txt dokumentet
                List<string> input = File.ReadAllLines(path).ToList(); //läser in total.txt till en lista av strings
                List<BackEnd.Report> reports = new List<BackEnd.Report>(); //gör en ny instans av reports

                foreach (var i in input) //loopar igenom listan
                {
                    var data = i.Split(","); //gör en array av varje inlägg i listan som splittas på ","
                    //skapar en ny rapport med datan i arrayen
                    reports.Add(new BackEnd.Report(int.Parse(data[0]), data[1], data[2], data[3], DateTime.Parse(data[4]), DateTime.Parse(data[5])));
                }

                MostPopularCage(reports);
                MosteExercisedHamster(reports);
                AverageWaitingTimeToExercise(reports);
                AverageTimesExercisedPerDay(reports);
            }
            else //om total.txt inte finns så sätts default värden
            {
                lbl_MostPopularCage.Text = "N/A";
                lbl_MostPopularCageID.Text = "N/A";
                lbl_MosteExercisedTimes.Text = "N/A";
                lbl_MostExercisedName.Text = "N/A";
                lbl_AverageWatingToExercise.Text = "N/A";
            }

        }
    }
}
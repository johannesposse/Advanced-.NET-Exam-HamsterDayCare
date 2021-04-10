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


        public FrmStatistics()
        {
            InitializeComponent();
        }

        private void Label_NumberOfSimulations_Click(object sender, EventArgs e)
        {

        }

        private void MostPopularCage(List<BackEnd.Report> reports)
        {
            var d = reports.GroupBy(x => x.Acticity).OrderByDescending(id => id.Count()).Select(g => new { Id = g.Key, Count = g.Count() });
            var b = d.Where(x => x.Id.Contains("Cage")).FirstOrDefault();
            lbl_MostPopularCage.Text = b.Id;
            lbl_MostPopularCageID.Text = b.Count.ToString() + " visits";
        }

        private void MosteExercisedHamster(List<BackEnd.Report> reports)
        {



            var hamsters = reports.Where(x => x.Acticity == " Exercise").GroupBy(x => x.Name).Select(x => new { Name = x.Key, Count = x.Count() });
            var mostExercised = hamsters.FirstOrDefault();
            lbl_MosteExercisedTimes.Text = "Has exercised " + mostExercised.Count.ToString() + " times";
            lbl_MostExercisedName.Text = mostExercised.Name;
        }

        private void FrmStatistics_Load(object sender, EventArgs e)
        {

            if (File.Exists(@"..\..\..\..\Logs\total.txt"))
            {
                string path = @"..\..\..\..\Logs\total.txt";
                List<string> input = File.ReadAllLines(path).ToList();
                List<BackEnd.Report> reports = new List<BackEnd.Report>();

                foreach (var i in input)
                {
                    var data = i.Split(",");
                    reports.Add(new BackEnd.Report(int.Parse(data[0]), data[1], data[2], data[3], DateTime.Parse(data[4]), DateTime.Parse(data[5])));
                }

                MostPopularCage(reports);
                MosteExercisedHamster(reports);
            }
            else
            {
                lbl_MostPopularCage.Text = "N/A";
                lbl_MostPopularCageID.Text = "N/A";
                lbl_MosteExercisedTimes.Text = "N/A";
                lbl_MostExercisedName.Text = "N/A";

            }

        }
    }
}
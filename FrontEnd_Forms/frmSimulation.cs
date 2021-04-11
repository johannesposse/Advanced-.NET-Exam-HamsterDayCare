using BackEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEnd_Forms
{
    public partial class FrmSimulation : Form
    {
        static BackEnd.HamsterDayCare hamsterDayCare = new BackEnd.HamsterDayCare();
        static Thread threadOne;
        string data;
        DateTime Date;
        public FrmSimulation()
        {
            InitializeComponent();
            hamsterDayCare.ReportEvent += ShowReport;
            data = "";
        }

        private void ShowReport(object sender, ReportEventArgs e)
        {
            MessageBox.Show("A new report has been created. Go to the reports tab to check it out");
            
            data = "";
        }

        private void ReceiveData(object sender, PrintEventArgs e)
        {
            data = e.Data;
            Date = e.Date;
        }

        private void StartSimulation()
        {
            
            BackEnd.HamsterDayCare.PrintEvent += ReceiveData;

            int numDays;
            int numSpeed;

            string path = @"..\..\..\..\config.txt";

            try
            {
                var config = File.ReadAllLines(path).ToList();
                var days = config[1].Split(",");
                numDays = int.Parse(days[1]);
                var speed = config[2].Split(",");
                numSpeed = int.Parse(speed[1]);
            }
            catch
            {
                MessageBox.Show("The config file was corrupted.\nRunning simulation at default values");
                numDays = 1;
                numSpeed = 10;
            }

            hamsterDayCare.StartSimulation(numDays, numSpeed);

            
            data = "";
            threadOne = null;
        }

        private void FrmSimulation_Load(object sender, EventArgs e)
        {
            threadOne = null;
        }

        private void TimerGetData_Tick(object sender, EventArgs e)
        {
            
            textBox1.Text = data;
            Label_Date.Text = Date.ToString("yyyy:MM:dd hh:mm:ss");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            if (threadOne == null)
            {
                threadOne = new Thread(new ThreadStart(StartSimulation));
                threadOne.Start();
                TimerGetData.Start();
            }
            else
            {
                MessageBox.Show("Stop that");
            }
        }
    }
}

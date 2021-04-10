using BackEnd;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
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
            TimerGetData.Stop();
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

            if (!int.TryParse(textBox2.Text, out numDays) | !int.TryParse(textBox3.Text, out numSpeed))
            {
                MessageBox.Show("This is a posetive number only field");
                return;
            }
            else if (numDays < 1 | numSpeed < 1)
            {
                MessageBox.Show("This is a posetive number only field");
                return;
            }

            hamsterDayCare.StartSimulation(numDays, numSpeed);
        }

        private void FrmSimulation_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            threadOne = new Thread(new ThreadStart(StartSimulation));
            threadOne.Start();
            TimerGetData.Start();
        }

        private void TimerGetData_Tick(object sender, EventArgs e)
        {
            textBox1.Text = data;
            Label_Date.Text = Date.ToString("yyyy:MM:dd hh:mm:ss");
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            threadOne = new Thread(new ThreadStart(StartSimulation));
            threadOne.Start();
            TimerGetData.Start();
        }
    }
}

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
        static BackEnd.HamsterDayCare hamsterDayCare;
        static Thread threadOne;
        string data;
        public FrmSimulation()
        {
            InitializeComponent();
        }


        private void ReceiveData(object sender, PrintEventArgs e)
        {
            data = e.Data;
        }

        private void StartSimulation()
        {
            BackEnd.HamsterDayCare.PrintEvent += ReceiveData;

            hamsterDayCare = new BackEnd.HamsterDayCare();
            hamsterDayCare.StartSimulation(int.Parse(textBox2.Text), int.Parse(textBox3.Text));
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
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            threadOne = new Thread(new ThreadStart(StartSimulation));
            threadOne.Start();
            TimerGetData.Start();
        }
    }
}

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
        string data; //för att kunna visa datan som skickas från backend utan att det ska bli konflikter med trådar
        DateTime Date;
        public FrmSimulation()
        {
            InitializeComponent();
            hamsterDayCare.ReportEvent += ShowReport; //sätter Showreport som lyssnare på report eventet
            data = ""; //nollställer
        }

        private void ShowReport(object sender, ReportEventArgs e) //anropas när en ny rapport genereras
        {
            MessageBox.Show("A new report has been created. Go to the reports tab to check it out"); //en messagebox visas
            
            data = ""; //nollställer data
        }

        private void ReceiveData(object sender, PrintEventArgs e) //anropas varje tick för att kunna skriva ut det som hänt
        {
            data = e.Data; //sätter värdena
            Date = e.Date;
        }

        private void StartSimulation() //metod för att starta ny simulering
        {
            
            BackEnd.HamsterDayCare.PrintEvent += ReceiveData; //sätter ReceiveData som lyssnare på PrintEvent varje tick

            int numDays;
            int numSpeed;

            string path = @"..\..\..\..\config.txt"; //sökväg för config filen

            try //försöker läsa in configfilen
            {
                var config = File.ReadAllLines(path).ToList();
                var days = config[1].Split(",");
                numDays = int.Parse(days[1]);
                var speed = config[2].Split(",");
                numSpeed = int.Parse(speed[1]);
            }
            catch //gick inte det så sätts defaultvärden och en messagebox visas
            {
                MessageBox.Show("The config file was corrupted.\nRunning simulation at default values");
                numDays = 1;
                numSpeed = 3;
            }

            hamsterDayCare.StartSimulation(numDays, numSpeed); //startar en ny simulering med de inlästa värderna från config fil (eller defualtvärden)

            
            data = ""; //nollställer
            threadOne = null; //när simuleringen är klar, så nullas tråden
        }

        private void FrmSimulation_Load(object sender, EventArgs e)
        {
            threadOne = null; //tråden nullas vid inladdning av formulär
        }

        private void TimerGetData_Tick(object sender, EventArgs e)
        {
            //en timer som ligger och tickar och sätter textbox och label till data(string) och date(datum)
            textBox1.Text = data;
            Label_Date.Text = Date.ToString("yyyy:MM:dd hh:mm:ss");
        }

        private void button1_Click_1(object sender, EventArgs e) //knapp för att starta simulering
        {
            if (threadOne == null) //kollar om tråden inte är aktiv
            {
                threadOne = new Thread(new ThreadStart(StartSimulation)); //initialiserar tråden Startsimulation metoden
                threadOne.Start(); //startar tråden
                TimerGetData.Start(); //startar timern som uppdaterar och visar data
            }
            else //är den aktiv visas en messagebox
            {
                MessageBox.Show("Stop that");
            }
        }
    }
}

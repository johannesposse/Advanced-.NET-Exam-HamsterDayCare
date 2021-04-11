using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEnd_Forms
{
    public partial class FrmReports : Form
    {
        static BackEnd.HamsterDayCare hamsterDayCare = new BackEnd.HamsterDayCare();
        static string[] options = hamsterDayCare.ShowPreviousResults(); //hämtar ut en array av föregående rapporter
        public FrmReports()
        {
            InitializeComponent();          

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmReports_Load(object sender, EventArgs e)
        {
            options = hamsterDayCare.ShowPreviousResults(); //hämtar ut en array av föregående rapporter
            if (options.Length < 1) //om den är tom så skrivs det ut en nolla
            {
                label4.Text = "0";
            }
            else //annars så skrivs antalet rapporter ut som antal simulationer (förutom total)
            {
                label4.Text = (options.Length - 1).ToString();
            }

            foreach (var o in options) //lägger till filerna i en listbox så användaren ska kunna välja vilken som ska visas
            {
                if(o != "total.txt") //total ska inte visas för användaren
                listBox1.Items.Add(o);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedItem = listBox1.SelectedIndex; //kollar vilket inlägg i listboxen som valts

            var printReport = new BackEnd.ReportEventArgs(); //gör en ny instans av ReporteventArgs

            printReport.PrintReports(@"..\..\..\..\Logs\" + options[selectedItem]); //generarar en rapport av den inlägget som valdes i listboxen

            textBox1.Text = printReport.Data; //skriver ut rapporten i textboxen
        }
    }
}

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
        static string[] options = hamsterDayCare.ShowPreviousResults();
        public FrmReports()
        {
            InitializeComponent();          

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmReports_Load(object sender, EventArgs e)
        {
            options = hamsterDayCare.ShowPreviousResults();
            if(options.Length < 1)
            {
                label4.Text = "0";
            }
            else
            {
                label4.Text = (options.Length - 1).ToString();
            }

            foreach (var o in options)
            {
                if(o != "total.txt")
                listBox1.Items.Add(o);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var selectedItem = listBox1.SelectedIndex;

            var printReport = new BackEnd.ReportEventArgs();

            printReport.PrintReports(@"..\..\..\..\Logs\" + options[selectedItem]);

            textBox1.Text = printReport.Data;
        }
    }
}

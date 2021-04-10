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
        public FrmReports()
        {
            InitializeComponent();


            string[] options = hamsterDayCare.ShowPreviousResults();
            label4.Text = options.Length.ToString();

            foreach (var o in options)
            {
                listBox1.Items.Add(o);
            }

            

            //var printReport = new BackEnd.ReportEventArgs();


            //printReport.PrintReports(@"..\..\..\..\Logs\" + options[selectedIndex]);

            //textBox1.Text = printReport.Data;


        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void FrmReports_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            string selectedItem = listBox1.Items[listBox1.SelectedIndex].ToString();

            MessageBox.Show(selectedItem);
        }
    }
}

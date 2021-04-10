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
    public partial class FrmDashBoard : Form
    {
        static BackEnd.HamsterDayCare hamsterDayCare = new BackEnd.HamsterDayCare();
        public FrmDashBoard()
        {
            InitializeComponent();
        }

        private void FrmDashBoard_Load(object sender, EventArgs e)
        {
            var options = hamsterDayCare.ShowPreviousResults();

            if (options.Length < 1)
            {
                Label_NumberOfSimulations.Text = "0";
            }
            else
            {
                Label_NumberOfSimulations.Text = (options.Length - 1).ToString();
            }
        }
    }
}

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
            var options = hamsterDayCare.ShowPreviousResults(); //hämtar ut en array av föregående rapporter

            if (options.Length < 1) //om den är tom så skrivs det ut en nolla
            {
                Label_NumberOfSimulations.Text = "0";
            }
            else //annars så skrivs antalet rapporter ut som antal simulationer (förutom total)
            {
                Label_NumberOfSimulations.Text = (options.Length - 1).ToString();
            }
        }
    }
}

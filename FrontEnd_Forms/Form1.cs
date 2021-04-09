using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BackEnd;

namespace FrontEnd_Forms
{
    public partial class HamsterDayCare : Form
    {
        private BackEnd.HamsterDayCare hamsterDay = new BackEnd.HamsterDayCare();
        public HamsterDayCare()
        {
            InitializeComponent();
            string userName = Environment.UserName;
            UserName.Text = "Welcome: " + userName;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void UserName_Click(object sender, EventArgs e)
        {
            
        }
    }
}

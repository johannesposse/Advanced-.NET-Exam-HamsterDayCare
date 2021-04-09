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
using System.Runtime.InteropServices;

namespace FrontEnd_Forms
{
    public partial class HamsterDayCare : Form
    {
        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]

       	 private static extern IntPtr CreateRoundRectRgn
         (
              int nLeftRect,
              int nTopRect,
              int nRightRect,
              int nBottomRect,
              int nWidthEllipse,
                 int nHeightEllipse

          );

        private BackEnd.HamsterDayCare hamsterDay = new BackEnd.HamsterDayCare();
        public HamsterDayCare()
        {
            InitializeComponent();
            Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 25, 25));

            string userName = Environment.UserName;
            UserName.Text = "Welcome " + userName;
        }

        private void Dashboard_button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Dashboard_button.Height;
            Nav_Pnl.Top = Dashboard_button.Top;
            Nav_Pnl.Left = Dashboard_button.Left;
            Dashboard_button.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void simulation_button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = simulation_button.Height;
            Nav_Pnl.Top = simulation_button.Top;
            Nav_Pnl.Left = simulation_button.Left;
            simulation_button.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void Reports_Button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Reports_Button.Height;
            Nav_Pnl.Top = Reports_Button.Top;
            Nav_Pnl.Left = Reports_Button.Left;
            Reports_Button.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void Statistics_Button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Statistics_Button.Height;
            Nav_Pnl.Top = Statistics_Button.Top;
            Nav_Pnl.Left = Statistics_Button.Left;
            Statistics_Button.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void Settings_Button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Settings_Button.Height;
            Nav_Pnl.Top = Settings_Button.Top;
            Nav_Pnl.Left = Settings_Button.Left;
            Settings_Button.BackColor = Color.FromArgb(46, 51, 73);
        }

        private void Dashboard_button_Leave(object sender, EventArgs e)
        {
            Dashboard_button.BackColor = Color.FromArgb(24,30,54);
        }

        private void simulation_button_Leave(object sender, EventArgs e)
        {
            simulation_button.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void Reports_Button_Leave(object sender, EventArgs e)
        {
            Reports_Button.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void Statistics_Button_Leave(object sender, EventArgs e)
        {
            Statistics_Button.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void Settings_Button_Leave(object sender, EventArgs e)
        {
            Settings_Button.BackColor = Color.FromArgb(24, 30, 54);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

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
using System.IO;

namespace FrontEnd_Forms
{
    public partial class HamsterDayCare : Form
    {
        static FrmSimulation frmSimulation = new FrmSimulation() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
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


            if (!Directory.Exists(@"..\..\..\..\Logs"))
            {
                Directory.CreateDirectory(@"..\..\..\..\Logs");
            }

            lblTitle.Text = "Welcome to the best Hamster Daycare, in the world";
            this.PnlFormLoader.Controls.Clear();
            FrmDashBoard frmDashBoard = new FrmDashBoard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmDashBoard.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmDashBoard);
            frmDashBoard.Show();
            string userName = Environment.UserName;
            UserName.Text = "Welcome " + userName;

        }

        private void Dashboard_button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Dashboard_button.Height;
            Nav_Pnl.Top = Dashboard_button.Top;
            Nav_Pnl.Left = Dashboard_button.Left;
            Dashboard_button.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Dashboard";
            this.PnlFormLoader.Controls.Clear();
            
            FrmDashBoard frmDashBoard = new FrmDashBoard() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmDashBoard.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmDashBoard);
            frmDashBoard.Show();
        }

        private void simulation_button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = simulation_button.Height;
            Nav_Pnl.Top = simulation_button.Top;
            Nav_Pnl.Left = simulation_button.Left;
            simulation_button.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Simulation";
            this.PnlFormLoader.Controls.Clear();
            
            frmSimulation.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmSimulation);
            frmSimulation.Show();
        }

        private void Reports_Button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Reports_Button.Height;
            Nav_Pnl.Top = Reports_Button.Top;
            Nav_Pnl.Left = Reports_Button.Left;
            Reports_Button.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Reports";
            this.PnlFormLoader.Controls.Clear();
            
            FrmReports frmReports = new FrmReports() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmReports.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmReports);
            frmReports.Show();
        }

        private void Statistics_Button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Statistics_Button.Height;
            Nav_Pnl.Top = Statistics_Button.Top;
            Nav_Pnl.Left = Statistics_Button.Left;
            Statistics_Button.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Statistics";
            this.PnlFormLoader.Controls.Clear();
            
            FrmStatistics frmStatistics = new FrmStatistics() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmStatistics.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmStatistics);
            frmStatistics.Show();
        }

        private void Settings_Button_Click(object sender, EventArgs e)
        {
            Nav_Pnl.Height = Settings_Button.Height;
            Nav_Pnl.Top = Settings_Button.Top;
            Nav_Pnl.Left = Settings_Button.Left;
            Settings_Button.BackColor = Color.FromArgb(46, 51, 73);

            lblTitle.Text = "Settings";
            this.PnlFormLoader.Controls.Clear();
            
            FrmSettings frmSettings = new FrmSettings() { Dock = DockStyle.Fill, TopLevel = false, TopMost = true };
            frmSettings.FormBorderStyle = FormBorderStyle.None;
            this.PnlFormLoader.Controls.Add(frmSettings);
            frmSettings.Show();
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

        private void PnlFormLoader_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FrontEnd_Forms
{
    public partial class FrmSettings : Form
    {
        static string path = @"..\..\..\..\config.txt";
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            if (!File.Exists(path))
            {
                using (File.Create(path)) { }
                string[] data = { "#Simulation", "days,1", "speed,10" };
                File.WriteAllLines(path, data);
                ReadFromFile();
            }
            else
            {
                ReadFromFile();
            }



        }

        private void ReadFromFile()
        {
            var config = File.ReadAllLines(path).ToList();
            try
            {
                var days = config[1].Split(",");
                txt_numDays.Text = days[1];
                var speed = config[2].Split(",");
                txt_NumSpeed.Text = speed[1];
            }
            catch
            {
                MessageBox.Show("The config file was corrupted.\nResetting the config file");
                string[] data = { "#Simulation", "days,1", "speed,10" };
                File.WriteAllLines(path, data);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            int numDays;
            int numSpeed;

            if (!int.TryParse(txt_numDays.Text, out numDays) | !int.TryParse(txt_NumSpeed.Text, out numSpeed))
            {
                MessageBox.Show("This is a posetive number only field");
                return;
            }
            else if (numDays < 1 | numSpeed < 1)
            {
                MessageBox.Show("This is a posetive number only field");
                return;
            }

            var data = File.ReadAllLines(path).ToList();
            data[1] = "days," + numDays;
            data[2] = "speed," + numSpeed;

            File.WriteAllLines(path, data);

            MessageBox.Show("The config file was updated");
        }

        private void btn_ResetConfig_Click(object sender, EventArgs e)
        {
            string[] data = { "#Simulation", "days,1", "speed,10" };
            File.WriteAllLines(path, data);
        }
    }
}

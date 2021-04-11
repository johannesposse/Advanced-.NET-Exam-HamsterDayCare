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
        static string path = @"..\..\..\..\config.txt"; //sökväg för config fil
        public FrmSettings()
        {
            InitializeComponent();
        }

        private void FrmSettings_Load(object sender, EventArgs e)
        {
            if (!File.Exists(path)) //kollar om filen finns, för den inte det så skapas den
            {
                using (File.Create(path)) { }
                string[] data = { "#Simulation", "days,1", "speed,3" };
                File.WriteAllLines(path, data);
                ReadFromFile(); //kallar på readfromfile
            }
            else //finns filen så kallas readfromfile
            {
                ReadFromFile();
            }
        }

        private void ReadFromFile()
        {
            var config = File.ReadAllLines(path).ToList(); //läser in configfilen
            try //försöker parsa informationen till textrutor
            {
                var days = config[1].Split(",");
                txt_numDays.Text = days[1];
                var speed = config[2].Split(",");
                txt_NumSpeed.Text = speed[1];
            }
            catch //om nåt blev fel, visas en messagebox och en ny configfil skapas
            {
                MessageBox.Show("The config file was corrupted.\nResetting the config file");
                string[] data = { "#Simulation", "days,1", "speed,3" };
                File.WriteAllLines(path, data);
            }
        }

        private void btn_Update_Click(object sender, EventArgs e) //knapp för att uppdatera configfilen
        {
            int numDays;
            int numSpeed;

            if (!int.TryParse(txt_numDays.Text, out numDays) | !int.TryParse(txt_NumSpeed.Text, out numSpeed)) //kollar om textrutorna inehåller posetiva intar
            {
                MessageBox.Show("This is a posetive number only field"); //gör de inte det visas en messagebox
                return;
            }
            else if (numDays < 1 | numSpeed < 1) //kollar så talen är posetiva
            {
                MessageBox.Show("This is a posetive number only field"); //gör de inte det visas en messagebox
                return;
            }

            var data = File.ReadAllLines(path).ToList(); //läser in configfilen till en array
            data[1] = "days," + numDays; //uppdaterar arrayen med de nya värden
            data[2] = "speed," + numSpeed; //uppdaterar arrayen med de nya värden

            File.WriteAllLines(path, data); //skriver de nya värdena till configfilen

            MessageBox.Show("The config file was updated"); //en messagebox visas
        }

        private void btn_ResetConfig_Click(object sender, EventArgs e) //knapp för att resetta configfilen
        {
            string[] data = { "#Simulation", "days,1", "speed,3" }; //defaultvärden
            File.WriteAllLines(path, data); //skriver över configfilen
        }
    }
}

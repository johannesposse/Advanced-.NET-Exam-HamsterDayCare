using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using BackEnd;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd
{
    class Program
    {
        static HamsterDayCare hamsterDayCare = new HamsterDayCare();

        static void Main(string[] args)
        {
            StartUpAnimation();
 
            hamsterDayCare.PrintEvent += Print;
            hamsterDayCare.ReportEvent += ShowReport;

            InitializeDataBase();


            try
            {
                UI();
            }
            catch(Exception x)
            {
                Console.WriteLine(x.Message);
                Console.ReadLine();
                UI();
            }
        }

        private static async Task StartUpAnimation()
        {
            Logo();

            var text = "  Probarbly the best daycare, in the world...";


            foreach (var t in text)
            {
                Console.Write(t);
                await Task.Delay(20);
            }

            await Task.CompletedTask;
        }

        private static void Print(object sender, PrintEventArgs e)
        {
            Console.CursorVisible = false;
            Console.SetCursorPosition(4, 10);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(e.Date + "\n" + e.Data);
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void ShowReport(object sender, ReportEventArgs e)
        {
            Console.WriteLine(e.Data);
            Console.WriteLine("Press any key to continue...");
            Console.ReadLine();
            Console.Clear();
        }
        private static int GenerateNumber(string message)
        {
            Console.CursorVisible = true;
            int num;
            bool intParse = int.TryParse(Console.ReadLine(), out num);

            if (!intParse)
            {
                throw new ArgumentOutOfRangeException(message);
            }
            else
            {
                return num;
            }


        }
        private static void UI()
        {
            Console.Clear();

            string prompt = "Welcome to the best daycare in the world"; //menyalternativ
            string[] options = {
                "Start new simulation",
                "Show reports from previous simulations",
                "Exit program"
               };

            Menu mainMenu = new Menu(prompt, options); //skickar in menyalternativerna till menu classen
            int selectedIndex = mainMenu.Run(); //får tillbaka vilken meny som valdes

            MenuChoise(selectedIndex);

        }
        private static void MenuChoise(int menu)
        {
            switch (menu)
            {
                case 0:
                    StartNewSimulation();
                    break;
               case 1:
                    ShowLogs();
                    break;
                case 2:
                    Environment.ExitCode = 0;
                break;

                default:
                    UI();
                    break;
            }
        }

        private static void ShowLogs()
        {
            Console.Clear();
            string[] options = hamsterDayCare.ShowPreviousResults();
            string prompt = "Welcome to the best daycare in the world"; //menyalternativ
 

            Menu mainMenu = new Menu(prompt, options); //skickar in menyalternativerna till menu classen
            int selectedIndex = mainMenu.Run(); //får tillbaka vilken meny som valdes

            var printReport = new ReportEventArgs();

            printReport.PrintReports(@"..\..\..\..\Logs\" + options[selectedIndex]);

            Console.Clear();
            Console.WriteLine(printReport.Data);

            Console.WriteLine("Press anykey to continue...");
            Console.ReadLine();
            UI();
        }

        private static void StartNewSimulation()
        {
            Console.Write("\nEnter number of days you want to simulate: ");
            int days = GenerateNumber("Please only enter numbers");

            Console.Write("Enter ticks per second: ");
            int speed = GenerateNumber("Please only enter numbers");

            Console.Clear();
            Logo();

            hamsterDayCare.StartSimulation(days, speed);

            UI();
        }
        private static void Logo()
        {
            string[] logo = {  

                "▒█░▒█ █▀▀█ █▀▄▀█ █▀▀ ▀▀█▀▀ █▀▀ █▀▀█ ▒█▀▀▄ █▀▀█ █░░█ ▒█▀▀█ █▀▀█ █▀▀█ █▀▀",
              "  ▒█▀▀█ █▄▄█ █░▀░█ ▀▀█ ░░█░░ █▀▀ █▄▄▀ ▒█░▒█ █▄▄█ █▄▄█ ▒█░░░ █▄▄█ █▄▄▀ █▀▀", 
              "  ▒█░▒█ ▀░░▀ ▀░░░▀ ▀▀▀ ░░▀░░ ▀▀▀ ▀░▀▀ ▒█▄▄▀ ▀░░▀ ▄▄▄█ ▒█▄▄█ ▀░░▀ ▀░▀▀ ▀▀▀"
            };

            Console.ForegroundColor = ConsoleColor.Blue;
            Console.SetCursorPosition(2, 3);
            foreach (var row in logo)
            {
                Console.WriteLine(row);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }
        private static void InitializeDataBase()
        {
            bool dbHasData = true;
            hamsterDayCare.InitilizeDatabase(out dbHasData);
        }
    }
}

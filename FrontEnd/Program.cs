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

            var text = "\nProbarbly the best daycare, in the world...";

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
            Logo();
            Console.WriteLine("Welcome to the best daycare in the world\n");

            Console.WriteLine("1. Start new simulation");
            Console.WriteLine("2. Show reports from previous simulations");
            Console.WriteLine("3. ---");
            Console.WriteLine("4. Exit program\n");

            int num = GenerateNumber("Please enter a number between 1-4");

            MenuChoise(num);

        }
        private static void MenuChoise(int menu)
        {
            switch (menu)
            {
                case 1:
                    StartNewSimulation();
                    break;
               case 2:
                    ShowLogs();
                    break;
                case 3:
                    UI();
                    break;
                case 4:
                    Environment.ExitCode = 0;
                break;

                default:
                    UI();
                    break;
            }
        }

        private static void ShowLogs()
        {
            var data = hamsterDayCare.ShowPreviousResults();
            foreach (var d in data)
            {
                Console.WriteLine(d);
            }


            Console.ReadLine();
            UI();
        }

        private static void StartNewSimulation()
        {
            Console.Write("Enter number of days you want to simulate: ");
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

"\n\n▒█░▒█ █▀▀█ █▀▄▀█ █▀▀ ▀▀█▀▀ █▀▀ █▀▀█ ▒█▀▀▄ █▀▀█ █░░█ ▒█▀▀█ █▀▀█ █▀▀█ █▀▀",
"▒█▀▀█ █▄▄█ █░▀░█ ▀▀█ ░░█░░ █▀▀ █▄▄▀ ▒█░▒█ █▄▄█ █▄▄█ ▒█░░░ █▄▄█ █▄▄▀ █▀▀", 
"▒█░▒█ ▀░░▀ ▀░░░▀ ▀▀▀ ░░▀░░ ▀▀▀ ▀░▀▀ ▒█▄▄▀ ▀░░▀ ▄▄▄█ ▒█▄▄█ ▀░░▀ ▀░▀▀ ▀▀▀"
            };

            Console.ForegroundColor = ConsoleColor.Blue;
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

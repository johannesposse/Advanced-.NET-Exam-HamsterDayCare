﻿using System;
using BackEnd;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd
{
    class Program
    {
        static HamsterDayCare hamsterDayCare = new HamsterDayCare();
        
        static void Main(string[] args)
        {
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
        private static void UI()
        {
            Console.Clear();
            Logo();
            Console.WriteLine("Welcome to the best daycare in the world\n");

            Console.WriteLine("1. Start new simulation");
            Console.WriteLine("2. Show logs from previous simulations");
            Console.WriteLine("3. Exit program\n");


            int menuChoise;
            bool intParse = int.TryParse(Console.ReadLine(), out menuChoise);

            if (!intParse)
            {
                throw new ArgumentOutOfRangeException("Please enter a number between 1-3");
            }
            else
            {
                MenuChoise(menuChoise);
            }

            
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
                    Environment.ExitCode = 0;
                break;

                default:
                    UI();
                    break;
            }
        }
        private static void ShowLogs()
        {
            throw new NotImplementedException();
        }
        private static void StartNewSimulation()
        {
            hamsterDayCare.StartSimulation(1, 4);
            Console.WriteLine(hamsterDayCare.Print());
        }
        private static void Logo()
        {
            string[] logo = {  "                           _                 ___              ___               ",
                              @"  /\  /\__ _ _ __ ___  ___| |_ ___ _ __     /   \__ _ _   _  / __\__ _ _ __ ___ ",
                              @" / /_/ / _` | '_ ` _ \/ __| __/ _ \ '__|   / /\ / _` | | | |/ /  / _` | '__/ _ \",
                              @"/ __  / (_| | | | | | \__ \ ||  __/ |     / /_// (_| | |_| / /__| (_| | | |  __/",
                              @"\/ /_/ \__,_|_| |_| |_|___/\__\___|_|    /___,' \__,_|\__, \____/\__,_|_|  \___|",
                              @"                                                      |___/                     "
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

            Console.CursorVisible = false;
            if (!dbHasData)
            {
                Console.SetCursorPosition(10, 5);
                string message = "No data in database, generating database...";
                foreach (var m in message)
                {
                    Console.Write(m);
                    System.Threading.Thread.Sleep(40);
                }

                Console.SetCursorPosition(17, 6);
                Console.WriteLine("[          ]");

                for (int i = 0; i < 10; i++)
                {
                    Console.SetCursorPosition(18+i, 6);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write("█");
                    System.Threading.Thread.Sleep(200);
                }

                System.Threading.Thread.Sleep(500);
                Console.Clear();
            }
            //else
            //{
            //    Console.SetCursorPosition(10, 5);
            //    string message = "Loading database...";
            //    foreach (var m in message)
            //    {
            //        Console.Write(m);
            //        System.Threading.Thread.Sleep(40);
            //    }
            //    System.Threading.Thread.Sleep(200);

            //    Console.SetCursorPosition(13, 6);
            //    Console.WriteLine("[          ]");

            //    for (int i = 0; i < 10; i++)
            //    {
            //        Console.SetCursorPosition(14 + i, 6);
            //        Console.ForegroundColor = ConsoleColor.Green;
            //        Console.Write("█");
            //        System.Threading.Thread.Sleep(200);
            //    }

            //    System.Threading.Thread.Sleep(500);
            //    Console.Clear();
            //    Console.CursorVisible = true;
            //}
        }
    }
}

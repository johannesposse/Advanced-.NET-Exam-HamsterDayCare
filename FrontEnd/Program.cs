using System;
using BackEnd;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd
{
    class Program
    {
        static HamsterDayCareContext hamsterDayCare = new HamsterDayCareContext();
        static void Main(string[] args)
        {
            InitializeDataBase();
            UI();
            
        }

        private static void UI()
        {
            Logo();
            Console.WriteLine("Welcome to the best daycare in the world\n");

            Console.WriteLine("1. Start new simulation");
            Console.WriteLine("2. Show logs from previous simulations");
            Console.WriteLine("3. Exit program\n");

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

            if (!dbHasData)
            {
                Console.WriteLine("No data in database, generating database");
            }
        }
    }
}

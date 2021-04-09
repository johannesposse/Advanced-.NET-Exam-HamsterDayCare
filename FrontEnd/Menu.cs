using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FrontEnd
{
    class Menu
    {
        private int SelIndex;
        private string[] Options;
        private string Prompt;


        public Menu(string prompt, string[] options) //tar emot indata och sätter propertys
        {
            Prompt = prompt;
            Options = options;
            SelIndex = 0;
        }

        private void DisplayOptions() //skriver ut menyalternativen
        {

            Console.ForegroundColor = ConsoleColor.White;
            Console.SetCursorPosition(2, 10);
            Console.WriteLine("    " + Prompt);

            for (int i = 0; i < Options.Length; i++)
            {

                string currentOption = Options[i];
                string prefix;
                if (i == SelIndex)
                {
                    prefix = "=>";
                    Console.ForegroundColor = ConsoleColor.White;
                    Console.BackgroundColor = ConsoleColor.DarkGreen;
                }
                else
                {
                    prefix = "  ";
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.BackgroundColor = ConsoleColor.Black;
                }

                Console.SetCursorPosition(2, i + 12);
                Console.WriteLine(prefix + " " + currentOption);
            }
            Console.ResetColor();
        }

        public int Run()
        {
            ConsoleKey keyPressed;
            do
            {
                Console.CursorVisible = false;
                string[] logo = new string[]
                {
                    "▒█░▒█ █▀▀█ █▀▄▀█ █▀▀ ▀▀█▀▀ █▀▀ █▀▀█ ▒█▀▀▄ █▀▀█ █░░█ ▒█▀▀█ █▀▀█ █▀▀█ █▀▀",
                    "▒█▀▀█ █▄▄█ █░▀░█ ▀▀█ ░░█░░ █▀▀ █▄▄▀ ▒█░▒█ █▄▄█ █▄▄█ ▒█░░░ █▄▄█ █▄▄▀ █▀▀",
                    "▒█░▒█ ▀░░▀ ▀░░░▀ ▀▀▀ ░░▀░░ ▀▀▀ ▀░▀▀ ▒█▄▄▀ ▀░░▀ ▄▄▄█ ▒█▄▄█ ▀░░▀ ▀░▀▀ ▀▀▀"
                };

                Console.SetCursorPosition(0, 0);
                for (int row = 0; row < logo.Length; row++)
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.SetCursorPosition(2, row + 3);

                    Console.Write(logo[row]);
                }
                DisplayOptions();

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                keyPressed = keyInfo.Key;

                if (keyPressed == ConsoleKey.DownArrow) //kollar om DownArrow trycks på och går neråt i menyn
                {
                    SelIndex++;
                    if (SelIndex > Options.Length - 1) //kollar så att man inte kan gå utanför menyalternativen
                        SelIndex = 0;
                }
                if (keyPressed == ConsoleKey.UpArrow) //kollar om UpArrow trycks på och går uppåt i menyn
                {
                    SelIndex--;
                    if (SelIndex < 0) //kollar så att man inte kan gå utanför menyalternativen
                        SelIndex = Options.Length - 1;
                }



            } while (keyPressed != ConsoleKey.Enter); //Väljer det markerade menyalternativet



            return SelIndex;
        }

    }
}

using System;
using BackEnd;
using Microsoft.EntityFrameworkCore;

namespace FrontEnd
{
    class Program
    {
        static HamsterDayCareContext hamsterDayCare;
        static void Main(string[] args)
        {
            hamsterDayCare = new HamsterDayCareContext();
            hamsterDayCare.AddHamster();
            //hamsterDayCare.Database.Migrate();
            Console.WriteLine("Hello World!");
        }
    }
}

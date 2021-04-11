using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Extensions
{
    public static class LinqExtensions
    {
          public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source) //extentionmetod till linq som blandar det IEnumerable objectet den får in
        {
            var ran = new Random();
            var sourceList = source.ToList(); //läser in det mottagna objectet till en lista
            var shuffeledList = new List<T>(); //skapar en ny generisk lista

            var listCount = sourceList.Count(); //läser av hur lång source listan är för att ha ett fast värde

            for (int i = 0; i < listCount; i++) //loopar igenom listan
            {
                var num = ran.Next(0, sourceList.Count()); //skapar ett slumpat tal från 0 till längden på listan (som kommer minska)
                shuffeledList.Add(sourceList.ElementAt(num)); //lägger till i den nya listan från en slumpad position från source listan
                sourceList.RemoveAt(num); //tar bort inlägget från source listan
                yield return shuffeledList.Last(); //retunerar det sista tillagde i den blandade listan för varje itterering
            }


        }
    }
}

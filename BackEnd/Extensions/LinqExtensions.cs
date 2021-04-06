using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackEnd.Extensions
{
    public static class LinqExtensions
    {
        public static IEnumerable<T> Random<T>(this IEnumerable<T> source)
        {
            var ran = new Random();
            yield return source.ElementAt(ran.Next(0, source.Count()));
        }

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
        {
            var ran = new Random();
            var sourceList = source.ToList();
            var shuffeledList = new List<T>();

            var listCount = sourceList.Count();

            for (int i = 0; i < listCount; i++)
            {
                var num = ran.Next(0, sourceList.Count());
                shuffeledList.Add(sourceList.ElementAt(num));
                sourceList.RemoveAt(num);
                yield return shuffeledList.Last();
            }


        }
    }
}

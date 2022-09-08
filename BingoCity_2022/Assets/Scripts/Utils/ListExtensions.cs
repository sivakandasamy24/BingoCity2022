using System;
using System.Collections.Generic;
using System.Linq;

namespace BingoCity
{
    
    public static class ListExtensions
    {
        public static List<T> GetClone<T>(this List<T> source) {
            return source.GetRange(0, source.Count);
        }
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;
            Random rnd = new Random();
            while (n > 1)
            {
                int k = (rnd.Next(0, n) % n);
                n--;
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetAndRemoveRandomValue<T>(this IList<T> list)
        {
            var randomNumber = list[UnityEngine.Random.Range(0, list.Count)];
            list.Remove(randomNumber);
            return randomNumber;
        }
    }
}
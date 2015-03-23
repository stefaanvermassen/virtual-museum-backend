using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VirtualMuseumAPI.Models;

namespace VirtualMuseumAPI.Helpers
{
    public static class VirtualMuseumUtils
    {

        public static T RandomIEnumerableElement<T>(this IEnumerable<T> source, Random rng)
        {
            T current = default(T);
            int count = 0;
            foreach (T element in source)
            {
                count++;
                if (rng.Next(count) == 0)
                {
                    current = element;
                }
            }
            return current;
        }

        
    }
}
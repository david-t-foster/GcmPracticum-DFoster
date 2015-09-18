using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GcmPracticum
{
    public static class Extensions
    {
        public static int IndexOf<T>(this IEnumerable<T> items, Func<T, bool> func)
        {
            return items.Select((item, index) => Tuple.Create(item, index))
                .Where(tp => func(tp.Item1))
                .Select(tp => tp.Item2)
                .First();
        }

        public static int IndexOf<T>(this IEnumerable<T> items, T expected) 
            where T : IEquatable<T>
        {
            return items.IndexOf(expected.Equals);
        }

        public static IEnumerable<T> TakeWhileInclusive<T>(this IEnumerable<T> items, Func<T, bool> func)
        {
            foreach (var t in items)
            {
                yield return t;
                if (!func(t))
                    break;
            } 
        }

        public static string Join<T>(this IEnumerable<T> items, string joiner)
        {
            return string.Join(joiner, items);
        }
    }
}

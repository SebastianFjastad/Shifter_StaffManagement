using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Collections.Generic
{
    /// <summary>
    /// Additional extensions on System.Collections.IEnumerable`1
    /// </summary>
    public static class IEnumerableExtensions
    {
        /// <summary>
        ///  Determines whether a sequence contains a specified element(s) by using a predicate
        /// </summary>
        public static bool Contains<TSource>(this IEnumerable<TSource> source, Func<TSource, bool> predicate)
        {
            return source.Where(predicate).Any();
        }

        /// <summary>
        /// Creates a randomized order enumerable from the source enumerable
        /// </summary>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> source)
        {
            if (source.IsNull())
            {
                return source;
            }

            var randomSort = new List<Tuple<int, T>>();

            var x = DateTime.Now.Millisecond + DateTime.Now.Second;

            foreach (var item in source)
            {
                var index = new Random(x).Next(1, int.MaxValue);
                randomSort.Add(new Tuple<int, T>(index, item));
                x++;
            }

            var sorted = (from r in randomSort
                          orderby r.Item1 ascending
                          select r.Item2).ToList();

            return sorted;
        }
    }
}
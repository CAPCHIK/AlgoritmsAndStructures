using System;
using System.Collections.Generic;
using System.Linq;

namespace LargestIncreasingSubsequence
{
    public static class GreebyAlgoritm
    {
        public static int[] Greedy(this IReadOnlyList<int> input)
        {
            var maxVal = (int)Math.Pow(2, input.Count);
            int[] best = null;
            for (var i = 0; i < maxVal; i++)
            {
                var current = i
                    .To2()
                    .Indexes()
                    .Select(index => input[index])
                    .ToIncreasing()
                    .ToArray();

                if (current.Length > (best?.Length ?? -1))
                    best = current;
#if DEBUG
                Console.Write($"\rgreeby meta max {maxVal - 1} current {i}");
#endif
            }
#if DEBUG
            var lastLength = Console.CursorLeft;
            Console.Write('\r');
            Console.Write(new string(' ', lastLength));
            Console.Write('\r');
#endif
            return best;
        }

        private static IEnumerable<int> ToIncreasing(this IEnumerable<int> sequence)
        {
            var start = true;
            var last = 0;
            foreach (var item in sequence)
            {
                if (start) {
                    last = item;
                    start = false;
                    yield return item;
                    continue;
                }
                if (item <= last) yield break;
                yield return item;
                last = item;
            }
        }


        private static IEnumerable<int> Indexes(this IEnumerable<int> in2)
        {
            var i = 0;
            foreach (var number in in2)
            {
                if (number == 1)
                    yield return i;
                i++;
            }
        }

        private static IEnumerable<int> To2(this int number)
        {
            while (number != 0)
            {
                yield return number % 2;
                number = number / 2;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;

namespace LargestIncreasingSubsequence
{
    public static class DynamicAlgoritm
    {
        public static IEnumerable<int> Dynamic(this IReadOnlyList<int> input)
        {
            if (input.Count < 2) return input;
            int[] js = new int[input.Count];
            js[0] = 1;
            var maxIndex = 0;
            for (var i = 1; i < js.Length; i++)
            {
                js[i] = input
                    .Take(i)
                    .Select((N, I) => (value: N, index: I))
                    .Where(P => P.value < input[i])
                    .Select(P => js[P.index])
                    .DefaultIfEmpty(0)
                    .Max() + 1;
                if (js[i] > js[maxIndex])
                    maxIndex = i;
#if DEBUG

                Console.Write($"\rgreeby meta max {js.Length - 1} current {i}");

#endif
            }
#if DEBUG
            var lastLength = Console.CursorLeft;
            Console.Write('\r');
            Console.Write(new string(' ', lastLength));
            Console.Write('\r');
#endif
            return input
                .Zip(js, (inp, j) => (value: inp, j: j))
                .Take(maxIndex + 1)
                .Reverse()
                .ToDiscreasing()
                .Reverse();
        }

        private static IEnumerable<int> ToDiscreasing(this IEnumerable<(int value, int j)> source)
        {
            var last = source.First();
            yield return last.value;
            foreach (var pair in source)
            {
                if (pair.value >= last.value || pair.j != last.j - 1) continue;
                yield return pair.value;
                last = pair;
            }
        }
    }
}
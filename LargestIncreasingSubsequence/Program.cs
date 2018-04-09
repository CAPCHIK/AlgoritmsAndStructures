using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace LargestIncreasingSubsequence
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            var separator = new string('-', 20);
            var currentList = Randoms().Take(10).ToArray();
            Console.WriteLine("For sample we have sequence with 10 elements");
            var watch = new Stopwatch();
            while (true)
            {
                Console.WriteLine("Sequence");
                Console.Write(string.Join(" ", currentList.Take(30)));
                Console.WriteLine(currentList.Length > 30 ? "..." : "");
                PrintMenu();
                if (int.TryParse(Console.ReadLine(), out var number))
                {
                    switch (number)
                    {
                        case 1:
                            Console.Write("Write length of sequence: ");
                            if (int.TryParse(Console.ReadLine(), out var length))
                                currentList = Randoms().Take(length).ToArray();
                            else
                            {
                                Console.WriteLine("incorrect line, recreating sequence");
                                currentList = Randoms().Take(currentList.Length).ToArray();
                            }

                            break;
                        case 2:
                            Console.WriteLine("Greedy algoritm:");
                            watch.Restart();
                            Console.WriteLine(string.Join(" ", currentList.Greedy()));
                            watch.Stop();
                            Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
                            break;
                        case 3:
                            Console.WriteLine("Dynamic algoritm:");
                            watch.Restart();
                            Console.WriteLine(string.Join(" ", currentList.Dynamic()));
                            watch.Stop();
                            Console.WriteLine($"Time: {watch.ElapsedMilliseconds}ms");
                            break;
                        case 4:
                            Console.Write(string.Join(" ", currentList));
                            break;
                        case 5:
                            return;
                        default:
                            Console.WriteLine("Write coorect command, please");
                            break;
                    }
                }
                Console.WriteLine(separator);
            }
        }

        private static void PrintMenu()
        {
            Console.WriteLine("1 - generate sequence");
            Console.WriteLine("2 - calc with greedy alg");
            Console.WriteLine("3 - calc with dynamic alg");
            Console.WriteLine("4 - show all sequence");
            Console.WriteLine("5 - exit program");
        }
        
        private static IEnumerable<int> Randoms()
        {
            var random = new Random();
            while (true)
            {
                yield return random.Next(100);
            }
        }
        
    }
}
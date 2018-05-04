using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            HashTable table = new HashTable(500, DivisionHash(499));
            var data = CreateData(1000);
            var workCounts = FillAndTest(table, data).ToArray();
            Console.WriteLine(workCounts.Min());
            Console.WriteLine(workCounts.Max());
            Console.WriteLine(workCounts.Average());
            table.Print();
            Console.WriteLine("Hello World!");
            Console.Read();
        }

        private static IEnumerable<int> FillAndTest(HashTable table, Data[] data)
        {
            foreach (var item in data)
            {
                table.Insert(item);
            }
            foreach(var item in data)
            {
                var (finded, workCount) = table.Find(item.key);
                yield return workCount;
            }
        }

        private static Func<int, int> DivisionHash(int divis)
            => a => a % divis;

        private static Data[] CreateData(int count, int seed = 123)
        {
            var random = new Random(seed);
            var data = new Data[count];
            for (int i = 0; i < count; i++)
            {
                data[i] = new Data { key = random.Next(1000, 100000), value = random.Next() };
            }
            return data;
        }
    }
}

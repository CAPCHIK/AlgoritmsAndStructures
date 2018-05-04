using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace HashTable
{
    class HashTable
    {
        private List<LinkedList<Data>> bags = new List<LinkedList<Data>>();
        private readonly Func<int, int> hashFunc;

        public HashTable(int n, Func<int, int> hashFunc)
        {
            for (int i = 0; i < n; i++)
            {
                bags.Add(new LinkedList<Data>());
            }
            this.hashFunc = hashFunc;
        }

        public bool Insert(Data data)
        {
            var hash = hashFunc(data.key);
            var position = hash % bags.Count;
            if (bags[position].Any(d => d.key == data.key))
                return false;
            bags[position].AddLast(data);
            return true;
        }

        public void Remove(int key)
        {
            var hash = hashFunc(key);
            var position = hash % bags.Count;
            foreach (var item in bags[position])
            {
                if (item.key == key)
                {
                    bags[position].Remove(item);
                    break;
                }
            }
        }

        public (Data data, int workCount) Find(int key)
        {
            var hash = hashFunc(key);
            var position = hash % bags.Count;
            int workCount = 2;
            foreach (var item in bags[position])
            {
                workCount += 1;
                if (item.key == key)
                {
                    return (item, workCount);
                }
            }
            return (default(Data), workCount);
        }

        public void Print()
        {
            Console.WriteLine($"Size {bags.Count}");
            for (int i = 0; i < bags.Count; i++)
            {
                Console.WriteLine($"Bag {i} contain {bags[i].Count} elements");
                foreach (var item in bags[i])
                {
                    Console.Write("--");
                    Console.WriteLine(item.ToString());
                }
            }
            Console.WriteLine(new string('-', 35));
        }
    }
}

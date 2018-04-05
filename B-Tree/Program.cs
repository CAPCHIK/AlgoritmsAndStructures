using System;
using System.Collections.Generic;

namespace B_Tree
{
    class Program
    {
        static List<string> somewords = new List<string>
        {
            "many",
            "words",
            "present",
            "very",
            "important",
            "data",
            "for",
            "soraging",
            "in",
            "B",
            "-",
            "Tree"
        };
        static Random random = new Random();
        static string RandomWord()
        {
            return "";
            return somewords[random.Next() % somewords.Count];
        }
        static void Main(string[] args)
        {

            var tree = new BTree<Data>(3);
            tree.Add(new Data(1, RandomWord()));
            tree.Add(new Data(3, RandomWord()));
            tree.Add(new Data(7, RandomWord()));
            tree.Add(new Data(10, RandomWord()));
            tree.Add(new Data(11, RandomWord()));
            tree.Add(new Data(13, RandomWord()));
            tree.Add(new Data(14, RandomWord()));
            tree.Add(new Data(15, RandomWord()));
            tree.Add(new Data(18, RandomWord()));
            tree.Add(new Data(16, RandomWord()));
            tree.Add(new Data(19, RandomWord()));
            tree.Add(new Data(24, RandomWord()));
            tree.Add(new Data(25, RandomWord()));
            tree.Add(new Data(26, RandomWord()));
            tree.Add(new Data(21, RandomWord()));
            tree.Add(new Data(4, RandomWord()));
            tree.Add(new Data(5, RandomWord()));
            tree.Add(new Data(20, RandomWord()));
            tree.Add(new Data(22, RandomWord()));
            tree.Add(new Data(2, RandomWord()));
            tree.Add(new Data(17, RandomWord()));
            tree.Add(new Data(12, RandomWord()));
            tree.Add(new Data(6, RandomWord()));
            tree.Print();
            Console.WriteLine(new string('-', 20));
            tree.Remove(26);
            tree.Print();
            Console.WriteLine(new string('-', 20));
            tree.Remove(3);
            tree.Print();
            Console.WriteLine(tree.Get(150));

            Console.Read();
        }
    }
}

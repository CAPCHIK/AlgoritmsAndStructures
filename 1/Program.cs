using System;
using System.Collections.Generic;

namespace _1
{
    class Program
    {
        static OneLinkList<Data> list = new OneLinkList<Data>();
        static Dictionary<int, Func<bool>> pathes = new Dictionary<int, Func<bool>>
        {
            { 1, AddItem },
            { 2, RemoveItem },
            { 3, ShowList },
            { 4, EndProgram }
        };
        static void Main(string[] args)
        {
            try
            {
                PrintMenu();
            }
            catch
            {
                Console.WriteLine("Program End"); 
            }
            Console.Read();
        }

        private static bool PrintMenu()
        {
            Console.WriteLine("1: add item");
            Console.WriteLine("2: remove item");
            Console.WriteLine("3: show list");
            Console.WriteLine("4: exit from program");
            int.TryParse(Console.ReadLine(), out var actionNumber);
            if (!pathes.TryGetValue(actionNumber, out var action) || !action())
                Console.WriteLine("Enter correct data");

            return PrintMenu();
        }

        private static bool AddItem()
        {
            Console.WriteLine("Enter key (number), value(string) and target position (number) separated space");
            Console.WriteLine("Example: key: 3 value: lol in position 2 (or in end)");
            Console.WriteLine("3 lol 2");
            var value = Console.ReadLine().Split(" ");
            if (value.Length != 3) return false;
            if (!int.TryParse(value[0], out var key)) return false;
            if (!int.TryParse(value[2], out var position)) return false;
            list.Add(new Data(key, value[1]), position);
            return true;
        }
        private static bool RemoveItem()
        {
            Console.WriteLine("Enter position(number) for removing element");
            if (!int.TryParse(Console.ReadLine(), out var position)) return false;
            list.Remove(position);
            return true;
        }
        private static bool ShowList()
        {
            Console.WriteLine(list);
            return true;
        }

        private static bool EndProgram() => throw new Exception();
    }
}

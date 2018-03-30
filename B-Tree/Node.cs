using System;
using System.Collections.Generic;
using System.Linq;

namespace B_Tree
{
    internal class Node<T> where T : class, IInTreeValue
    {
        public List<T> values;
        public List<Node<T>> childs;

        public int t;
        public bool leaf;
        public int n;
        public Node(int t, bool leaf)
        {

            this.t = t;
            this.leaf = leaf;

            values = new List<T>(Enumerable.Repeat<T>(null, 2 * t - 1));
            childs = new List<Node<T>>(Enumerable.Repeat<Node<T>>(null, 2 * t));
            n = 0;
        }

        public Node<T> Search(int key)
        {
            int i = 0;
            while (i < n && key > values[i].Key)
                i++;
            if (values.Get(i)?.Key == key)
                return this;

            return childs[i]?.Search(key);
        }

        internal void InsertNonFull(T k)
        {
            int i = n - 1;

            if (leaf == true)
            {
                while (i >= 0 && values[i].Key > k.Key)
                {
                    values[i + 1] = values[i];
                    i--;
                }

                values[i + 1] = k;
                n = n + 1;
            }
            else
            {
                while (i >= 0 && values[i].Key > k.Key)
                    i--;

                if (childs[i + 1].n == 2 * t - 1)
                {
                    SplitChild(i + 1, childs[i + 1]);

                    if (values[i + 1].Key < k.Key)
                        i++;
                }
                childs[i + 1].InsertNonFull(k);
            }
        }

        internal void SplitChild(int i, Node<T> y)
        {
            var z = new Node<T>(y.t, y.leaf)
            {
                n = t - 1
            };

            for (int j = 0; j < t - 1; j++)
                z.values[j] = y.values[j + t];

            if (y.leaf == false)
            {
                for (int j = 0; j < t; j++)
                    z.childs[j] = y.childs[j + t];
            }

            y.n = t - 1;

            for (int j = n; j >= i + 1; j--)
                childs[j + 1] = childs[j];

            childs[i + 1] = z;

            for (int j = n - 1; j >= i; j--)
                values[j + 1] = values[j];

            values[i] = y.values[t - 1];

            n = n + 1;
        }

        internal void Print(int deep)
        {
            int i = 0;
            for (; i < n; i++)
            {
                Console.Write(new string('=', deep));
                childs[i]?.Print(deep + 1);
                Console.WriteLine(values[i]?.ToString());
            }
            childs[i]?.Print(deep + 1);
        }
    }
}
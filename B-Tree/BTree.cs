using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace B_Tree
{
    class BTree<T> where T : class, IInTreeValue
    {
        private readonly int t;
        private Node<T> root;
        public BTree(int t)
        {
            this.t = t;
        }
        public void Add(T value)
        {
            if (root == null)
            {
                root = new Node<T>(t, true);
                root.values[0] = value;
                root.n = 1;
                return;
            }
            if (root.n == 2 * t - 1)
            {
                var node = new Node<T>(t, false);

                node.childs[0] = root;

                node.SplitChild(0, root);

                int i = 0;
                if (node.values[0].Key < value.Key)
                    i++;
                node.childs[i].InsertNonFull(value);


                root = node;
                return;
            }
            root.InsertNonFull(value);
        }

        public void Remove(int k)
        {
            if (root == null)
            {
                Console.WriteLine("The tree is empty");
                return;
            }

            root.Remove(k);

            if (root.n == 0)
            {
                var tmp = root;
                if (root.leaf)
                    root = null;
                else
                    root = root.childs[0];
            }
            return;
        }



        public void Print()
        {
            if (root == null) return;
            root.Print(0);
        }

        public T Get(int key)
        {
            return root?.Search(key)?.values?.FirstOrDefault(V => V.Key == key);
        }
    }
}

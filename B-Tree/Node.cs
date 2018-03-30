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
        public bool leaf; // Is true when node is leaf. Otherwise false
        public int n;     // Current number of values
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
            // Initialize index as index of rightmost element
            int i = n - 1;

            // If this is a leaf node
            if (leaf == true)
            {
                // The following loop does two things
                // a) Finds the location of new key to be inserted
                // b) Moves all greater values to one place ahead
                while (i >= 0 && values[i].Key > k.Key)
                {
                    values[i + 1] = values[i];
                    i--;
                }

                // Insert the new key at found location
                values[i + 1] = k;
                n = n + 1;
            }
            else // If this node is not leaf
            {
                // Find the child which is going to have the new key
                while (i >= 0 && values[i].Key > k.Key)
                    i--;

                // See if the found child is full
                if (childs[i + 1].n == 2 * t - 1)
                {
                    // If the child is full, then split it
                    SplitChild(i + 1, childs[i + 1]);

                    // After split, the middle key of C[i] goes up and
                    // C[i] is splitted into two.  See which of the two
                    // is going to have the new key
                    if (values[i + 1].Key < k.Key)
                        i++;
                }
                childs[i + 1].InsertNonFull(k);
            }
        }

        internal void SplitChild(int i, Node<T> y)
        {
            // Create a new node which is going to store (t-1) values
            // of y
            var z = new Node<T>(y.t, y.leaf)
            {
                n = t - 1
            };

            // Copy the last (t-1) values of y to z
            for (int j = 0; j < t - 1; j++)
                z.values[j] = y.values[j + t];

            // Copy the last t children of y to z
            if (y.leaf == false)
            {
                for (int j = 0; j < t; j++)
                    z.childs[j] = y.childs[j + t];
            }

            // Reduce the number of values in y
            y.n = t - 1;

            // Since this node is going to have a new child,
            // create space of new child
            for (int j = n; j >= i + 1; j--)
                childs[j + 1] = childs[j];

            // Link the new child to this node
            childs[i + 1] = z;

            // A key of y will move to this node. Find location of
            // new key and move all greater values one space ahead
            for (int j = n - 1; j >= i; j--)
                values[j + 1] = values[j];

            // Copy the middle key of y to this node
            values[i] = y.values[t - 1];

            // Increment count of values in this node
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
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

        public int FindKey(int k)
        {
            int i = 0;
            while (i < n && values[i].Key < k)
                ++i;
            return i;
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

        public void Remove(int key)
        {
            int idx = FindKey(key);

            if (idx < n && values[idx].Key == key)
            {
                if (leaf)
                    RemoveFromLeaf(idx);
                else
                    RemoveFromNonLeaf(idx);
            }
            else
            {

                if (leaf)
                {
                    Console.WriteLine($"The key {key} is does not exist in the tree");
                    return;
                }


                bool flag = ((idx == n) ? true : false);

                if (childs[idx].n < t)
                    Fill(idx);

                if (flag && idx > n)
                    childs[idx - 1].Remove(key);
                else
                    childs[idx].Remove(key);
            }
        }
        private void RemoveFromLeaf(int idx)
        {
            for (int i = idx + 1; i < n; ++i)
                values[i - 1] = values[i];
            n--;

            return;
        }
        private void RemoveFromNonLeaf(int idx)
        {

            int k = values[idx].Key;

            if (childs[idx].n >= t)
            {
                var pred = GetPred(idx);
                values[idx] = pred;
                childs[idx].Remove(pred.Key);
            }
            else if (childs[idx + 1].n >= t)
            {
                var succ = GetSucc(idx);
                values[idx] = succ;
                childs[idx + 1].Remove(succ.Key);
            }

            else
            {
                Merge(idx);
                childs[idx].Remove(k);
            }
            return;
        }

        private T GetPred(int idx)
        {
            var cur = childs[idx];
            while (!cur.leaf)
                cur = cur.childs[cur.n];

            return cur.values[cur.n - 1];
        }

        private T GetSucc(int idx)
        {

            
            var cur = childs[idx + 1];
            while (!cur.leaf)
                cur = cur.childs[0];

            return cur.values[0];
        }

        private void Fill(int idx)
        {

            if (idx != 0 && childs[idx - 1].n >= t)
                BorrowFromPrev(idx);

            else if (idx != n && childs[idx + 1].n >= t)
                BorrowFromNext(idx);

            else
            {
                if (idx != n)
                    Merge(idx);
                else
                    Merge(idx - 1);
            }
            return;
        }

        private void BorrowFromPrev(int idx)
        {

            var child = childs[idx];
            var sibling = childs[idx - 1];


            for (int i = child.n - 1; i >= 0; --i)
                child.values[i + 1] = child.values[i];

            if (!child.leaf)
            {
                for (int i = child.n; i >= 0; --i)
                    child.childs[i + 1] = child.childs[i];
            }

            child.values[0] = values[idx - 1];

            if (!leaf)
                child.childs[0] = sibling.childs[sibling.n];

            values[idx - 1] = sibling.values[sibling.n - 1];

            child.n += 1;
            sibling.n -= 1;

            return;
        }

        private void BorrowFromNext(int idx)
        {

            var child = childs[idx];
            var sibling = childs[idx + 1];

            child.values[(child.n)] = values[idx];

            if (!(child.leaf))
                child.childs[(child.n) + 1] = sibling.childs[0];

            values[idx] = sibling.values[0];

            for (int i = 1; i < sibling.n; ++i)
                sibling.values[i - 1] = sibling.values[i];

            if (!sibling.leaf)
            {
                for (int i = 1; i <= sibling.n; ++i)
                    sibling.childs[i - 1] = sibling.childs[i];
            }

            child.n += 1;
            sibling.n -= 1;

            return;
        }


        private void Merge(int idx)
        {
            var child = childs[idx];
            var sibling = childs[idx + 1];

            child.values[t - 1] = values[idx];

            for (int i = 0; i < sibling.n; ++i)
                child.values[i + t] = sibling.values[i];

            if (!child.leaf)
            {
                for (int i = 0; i <= sibling.n; ++i)
                    child.childs[i + t] = sibling.childs[i];
            }

            for (int i = idx + 1; i < n; ++i)
                values[i - 1] = values[i];

            for (int i = idx + 2; i <= n; ++i)
                childs[i - 1] = childs[i];

            child.n += sibling.n + 1;
            n--;
            return;
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
                childs[i]?.Print(deep + 1);
                Console.Write(new string('=', deep));
                Console.WriteLine(values[i]?.ToString());
            }
            childs[i]?.Print(deep + 1);
        }
    }
}
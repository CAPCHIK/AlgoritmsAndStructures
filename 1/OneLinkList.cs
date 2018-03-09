using System;
using System.Collections.Generic;
using System.Text;

namespace _1
{
    class OneLinkList<T>
    {
        private Node<T> head;
        
        public void Add(T data, int position)
        {
            var node = new Node<T>(data);
            if (head == null)
            {
                head = node;
                return;
            }
            var needNode = Find(position);
            node.Next = needNode.Next;
            needNode.Next = node;
        }

        public void Remove(int position)
        {
            var preTarget = Find(position);
            if (position == 0)
            {
                head = preTarget.Next;
                preTarget.Next = null;
            }
            preTarget.Next = preTarget.Next?.Next;
        }

        private Node<T> Find(int position)
        {
            var current = head;
            for(int i = 1; i < position; i++)
            {
                if (current.Next == null) return current;
                current = current.Next;
            }
            return current;
        }


        public override string ToString()
        {
            var current = head;
            int i = 0;
            var builder = new StringBuilder();
            while (current != null)
            {
                builder.AppendLine($"{i++}: {current.Data}");
                current = current.Next;
            }
            return builder.ToString();
        }


        private class Node<V>
        {
            public Node<V> Next { get; set; }
            public V Data { get; set; }
            public Node(V value)
            {
                Data = value;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace B_Tree
{
    class BTree<T> where T : IInTreeValue
    {
        private Node<T> head = new Node<T>();
        public void Add(IInTreeValue value)
        {
            throw new NotImplementedException();
        }

        public T Get(int key)
        {
            throw new NotImplementedException();
        }
    }
}

using System.Collections.Generic;

namespace B_Tree
{
    internal class Node<T> where T : IInTreeValue
    {
        private List<T> values = new List<T>();
        private List<Node<T>> childs = new List<Node<T>>();
    }
}
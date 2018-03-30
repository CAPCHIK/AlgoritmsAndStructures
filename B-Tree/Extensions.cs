using System;
using System.Collections.Generic;
using System.Text;

namespace B_Tree
{
    static class Extensions
    {
        public static T Get<T>(this List<T> list, int position)
        {
            if (position >= list.Count)
                return default(T);
            return list[position];
        }
    }
}

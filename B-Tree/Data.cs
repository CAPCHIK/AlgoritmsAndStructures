using System;
using System.Collections.Generic;
using System.Text;

namespace B_Tree
{
    class Data : IInTreeValue
    {
        public int Key { get; set; }
        public string Value { get; }

        public Data(int key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $">{Key}-{Value}<";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace _1
{
    class Data
    {
        public int Key { get; set; }
        public string Value { get; set; }

        public Data(int key, string value)
        {
            Key = key;
            Value = value;
        }

        public override string ToString()
        {
            return $"Key: {Key} Value: {Value}";
        }
    }
}

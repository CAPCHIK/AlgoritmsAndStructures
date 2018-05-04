using System;
using System.Collections.Generic;
using System.Text;

namespace HashTable
{
    struct Data
    {
        public int key;
        public int value;
        public override string ToString()
        {
            return $"KEY {key} VALUE {value}";
        }
    }
}

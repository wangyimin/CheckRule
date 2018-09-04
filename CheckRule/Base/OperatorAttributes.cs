using System;

namespace Base
{
    class OperatorAttributes : Attribute
    {
        public string Display { get; set; }
        public Type Comparison { get; set; }
    }
}

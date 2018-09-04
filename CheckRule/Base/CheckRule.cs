using System;
using System.Reflection;
using System.Linq;

namespace Base
{
    public class CheckRule
    {
        private Operator _operator;
        private object[] _value;

        public CheckRule(Operator _operator, object _value)
        {
            this._operator = _operator;

            if (_value.GetType().IsArray)
            {
                object[] val = _value as object[];
                this._value = new object[val.Length];
                val.CopyTo(this._value, 0);
            }
            else
            {
                this._value = new object[1];
                this._value[0] = _value;
            }
        }

        public override string ToString()
        {
            string r = _operator == Operator.IN ? " " + _operator.Display() + " [" : _operator.Display();

            _value.ToList().ForEach(el => { r = r + el + ","; });

            return r.Substring(0, r.Length - 1) + (_operator == Operator.IN ? "]" : "");
        }

        public bool GetCheckResult(object target)
        {
            IComparison impl = new CommComparison();

            OperatorAttributes attr = _operator.GetType().GetMember(_operator.ToString())
                            .First()
                            .GetCustomAttribute<OperatorAttributes>();

            if (attr != null && attr.Comparison != null)
            {
                impl = (IComparison)Activator.CreateInstance(Type.GetType(attr.Comparison.FullName));
            }

            return impl.Compare(_operator, target, _value);
        }
    }
}

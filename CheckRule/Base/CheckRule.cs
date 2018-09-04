using System;
using System.Reflection;
using System.Linq;

namespace Base
{
    public class CheckRule
    {
        private Operator _operator;
        private object _value;

        public CheckRule(Operator _operator, object _value)
        {
            this._operator = _operator;
            this._value = _value;
        }

        public override string ToString()
        {
            return _operator.Display() 
                + (_value is IItem ? ((IItem)_value).GetValue() : _value);
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

            if (Operator.GT.Equals(_operator))
            {
                return impl.Compare(target, _value) > 0;
            }
            else if (Operator.GE.Equals(_operator))
            {
                return impl.Compare(target, _value) >= 0;
            }
            else if (Operator.LT.Equals(_operator))
            {
                return impl.Compare(target, _value) < 0;
            }
            else if (Operator.LE.Equals(_operator))
            {
                return impl.Compare(target, _value) <= 0;
            }
            else if (Operator.EQ.Equals(_operator))
            {
                return impl.Compare(target, _value) == 0;
            }
            else
            {
                throw new NotSupportedException("Unsupported logic operator[" + _operator + "].");
            }
        }
    }
}

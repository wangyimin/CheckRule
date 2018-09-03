using System;

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
            if (Operator.GT.Equals(_operator))
            {
                return _compare(target, _value) > 0;
            }
            else if (Operator.GE.Equals(_operator))
            {
                return _compare(target, _value) >= 0;
            }
            else if (Operator.LT.Equals(_operator))
            {
                return _compare(target, _value) < 0;
            }
            else if (Operator.LE.Equals(_operator))
            {
                return _compare(target, _value) <= 0;
            }
            else if (Operator.EQ.Equals(_operator))
            {
                return _compare(target, _value) == 0;
            }
            else
            {
                throw new NotSupportedException("Unsupported logic operator[" + _operator + "].");
            }
        }

        private int _compare(object _v1, object _v2)
        {
            if (_v1 == null || _v2 == null)
            {
                throw new ArgumentNullException("Null parameter.");
            }

            if (_v1.GetType() != _v2.GetType())
            {
                throw new InvalidOperationException("Unable to comapre different type.");
            }

            if (_v1.GetType() == typeof(int))
            {
                return ((int)_v1).CompareTo((int)_v2);
            }
            else if (_v1.GetType() == typeof(long))
            {
                return ((long)_v1).CompareTo((long)_v2);
            }
            else if (_v1.GetType() == typeof(DateTime))
            {
                return (((DateTime)_v1).ToBinary()).CompareTo(((DateTime)_v2).ToBinary());
            }
            else
            {
                throw new NotSupportedException("Unsupported compare type[" + _v1.GetType().Name + "].");
            }
        }
    }
}

using System;
using System.Linq;

namespace Base
{
    public interface IComparison
    {
        bool Compare(Operator _operator, object _v1, object _v2);
    }

    public class CommComparison : IComparison
    {
        public bool Compare(Operator _operator, object _v1, object _v2)
        {
            if (_v1 == null || _v2 == null)
            {
                throw new ArgumentNullException("Null parameter.");
            }

            object[] val = _v2 as object[];
            if (Operator.GT.Equals(_operator))
            {
                return _compare(_v1, val[0]) > 0;
            }
            else if (Operator.GE.Equals(_operator))
            {
                return _compare(_v1, val[0]) >= 0;
            }
            else if (Operator.LT.Equals(_operator))
            {
                return _compare(_v1, val[0]) < 0;
            }
            else if (Operator.LE.Equals(_operator))
            {
                return _compare(_v1, val[0]) <= 0;
            }
            else if (Operator.EQ.Equals(_operator))
            {
                return _compare(_v1, val[0]) == 0;
            }
            else
            {
                throw new NotSupportedException("Unsupported logic operator[" + _operator + "].");
            }
        }

        private int _compare(object _v1, object _v2)
        { 
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

    public class InComparison : IComparison
    {
        public bool Compare(Operator _operator, object _v1, object _v2)
        {
            if (_v1 == null || _v2 == null)
            {
                throw new ArgumentNullException("Null parameter.");
            }

            if (Operator.IN != _operator)
            {
                throw new InvalidOperationException("Unsupported operator[" + _operator.Display() + "].");
            }

            object[] val = _v2 as object[];

            if (_v1.GetType() == typeof(int))
            {
                return val.ToList().Where(el => (int)_v1 == (int)el).Count() != 0;
            }
            else if (_v1.GetType() == typeof(long))
            {
                return val.ToList().Where(el => (long)_v1 == (long)el).Count() != 0;
            }
            else
            {
                throw new NotSupportedException("Unsupported compare type[" + _v1.GetType().Name + "].");
            }
        }
    }
}

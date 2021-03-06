﻿using System;
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
            if (_v1 != null && _v2 != null && _v1.GetType() != _v2.GetType())
                throw new InvalidOperationException("Need same type");

            if (_v1 != null)
                return (int)_v1.GetType().GetMethod("CompareTo", new Type[] { _v1.GetType() }).Invoke(_v1, new object[] { _v2 });
            else if (_v2 != null)
                return -1;
            else
                return 0;
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
                throw new InvalidOperationException("Need operator[IN]");
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
    public class BetweenComparison : IComparison
    {
        public bool Compare(Operator _operator, object _v1, object _v2)
        {
            if (_v1 == null || _v2 == null)
            {
                throw new ArgumentNullException("Null parameter.");
            }

            if (Operator.BETWEEN != _operator)
            {
                throw new InvalidOperationException("Need operator[BETWEEN].");
            }

            object[] val = _v2 as object[];
            if (val.Length != 2)
            {
                throw new InvalidOperationException("Need two values for operator[BETWEEN].");
            }

            if (_v1.GetType() == typeof(int))
            {
                return (int)_v1 >= (int)val[0] && (int)_v1 <= (int)val[1];
            }
            else if (_v1.GetType() == typeof(long))
            {
                return (long)_v1 >= (long)val[0] && (long)_v1 <= (long)val[1];
            }
            else
            {
                throw new NotSupportedException("Unsupported compare type[" + _v1.GetType().Name + "].");
            }
        }
    }
}

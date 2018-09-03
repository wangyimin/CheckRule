using System;
using System.Collections.Generic;
using System.Reflection;

namespace Base
{
    public class CheckRules
    {
        private Queue<object> _que = new Queue<object>();
        public CheckRules Add<T>(Operator _operator, T _check)
        {
            _que.Enqueue(new Expression<T>(_operator, _check));
            return this;
        }

        public string GetExpression(object target)
        {
            string r = "(";

            foreach (var el in _que)
            {
                Type type = el.GetType().GetGenericArguments()[0];
                r = r + typeof(Extensions).GetMethod("GetLogic").MakeGenericMethod(type).Invoke(null, new object[] { el, target }) + " ";
            }

            r = r.Substring(0, r.Length - 1) + ")";

            return r;
        }

        public bool GetCheckResult(object target)
        {
            bool r = true;

            foreach (var el in _que)
            {
                Type type = el.GetType().GetGenericArguments()[0];

                Operator _operator = (Operator)(el.GetType().GetMethod("GetOperator", BindingFlags.Public | BindingFlags.Instance).Invoke(el, null));

                if (Operator.NONE.Equals(_operator))
                {
                    r = _wrapper("_check", new object[] { el, type, target });
                }
                else if (Operator.AND.Equals(_operator))
                {
                    r = r && _wrapper("_check", new object[] { el, type, target });
                }
                else if (Operator.OR.Equals(_operator))
                {
                    r = r || _wrapper("_check", new object[] { el, type, target });
                }
                else
                {
                    throw new NotSupportedException("Unsupported logical operator[" + _operator + "].");
                }
            }

            return r;
        }

        private bool _wrapper(string method, params object[] para)
        {
            return (bool)(this.GetType()
                .GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance)
                .MakeGenericMethod((Type)para[1])
                .Invoke(this, new object[] { para[0], para[2] }));
        }

        private bool _check<T>(Expression<T> el, object target)
        {
            Type type = el.GetType().GetGenericArguments()[0];

            if (type == typeof(CheckRule))
            {
                return ((CheckRule)(object)(el.GetCheck())).GetCheckResult(target);
            }
            else if (type == typeof(CheckRules))
            {
                return ((CheckRules)(object)(el.GetCheck())).GetCheckResult(target);
            }
            else
            {
                throw new InvalidOperationException("Unsupported type [" + type.Name + "].");
            }
        }

    }

    public class Expression<T>
    {
        private Operator _operator;
        private T _check;

        public  Expression(Operator _operator, T _check)
        {
            this._operator = _operator;
            this._check = _check;
        }

        public Operator GetOperator()
        {
            return _operator;
        }

        public T GetCheck()
        {
            return _check;
        }

    }
}

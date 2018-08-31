using System;
using System.Collections.Generic;

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

            while (_que.Count > 0)
            {
                var el = _que.Dequeue();

                Type type = el.GetType().GetGenericArguments()[0];
                r = r + typeof(Extensions).GetMethod("Convert").MakeGenericMethod(type).Invoke(null, new object[] { el, target }) + " ";
            }

            r = r.Substring(0, r.Length - 1) + ")";

            return r;
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

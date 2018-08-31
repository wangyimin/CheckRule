using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using System.Threading.Tasks;

namespace Base
{
    public static class Extensions
    {
        public static string Display(this Enum e)
        {
            return e.GetType()
                            .GetMember(e.ToString())
                            .First()
                            .GetCustomAttribute<OperatorAttributes>()
                            .Display;
        }

        public static object Convert(this CheckRule check, object target)
        {
            return _convert(target);
        }

        public static bool HasValidValue(this IItem obj)
        {
            string s = obj.GetCheckRules().GetExpression(_convert(obj.GetValue()));
            Trace.WriteLine(s);

            return _compute(s);
        }

        private static object _convert(object obj)
        {
            if (obj.GetType().Assembly == Assembly.GetExecutingAssembly())
            {
                throw new NotSupportedException("Unsupported object type");
            }

            if (obj.GetType() == typeof(DateTime))
            {
                return ((DateTime)obj).ToBinary();
            }
            else if (obj.GetType() == typeof(string))
            {
                throw new NotSupportedException("Unsupported object type");

            }
            else
            {
                return obj;
            }
        }

        private static bool _compute(string s)
        {
            Task<bool> r = CSharpScript.EvaluateAsync<bool>(s);
            r.Wait();

            return r.Result;
        }
  
        public static bool IsNotNull(this IItem obj)
        {
            return obj.GetValue() != null;
        }

        public static string GetLogic<T>(this Expression<T> el, object target)
        {
            Type type = el.GetType().GetGenericArguments()[0];

            Operator _operator = el.GetOperator();

            if (type == typeof(CheckRule))
            {
                return (Operator.NONE == _operator ? "" : _operator.Display() + " ") + target + el.GetCheck().ToString();
            }
            else if (type == typeof(CheckRules))
            {
                return (Operator.NONE == _operator ? "" : _operator.Display() + " ") + ((CheckRules)(object)(el.GetCheck())).GetExpression(target);
            }
            else
            {
                throw new InvalidOperationException("Bad element.");
            }
        }
    }
}

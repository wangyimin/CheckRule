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

        public static bool HasValidValue(this IItem obj)
        {
            string s = obj.GetCheckRules().GetExpression(obj.GetValue());
            Trace.WriteLine(s);

            return _compute(s);
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

        public static string Convert<T>(this Expression<T> el, object target)
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

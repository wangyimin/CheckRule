using System;
using System.Linq;
using System.Reflection;

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

        public static string GetExpression(this IItem obj)
        {
            return obj.GetCheckRules().GetExpression(obj.GetValue());
        }

        public static bool IsNotNull(this IItem obj)
        {
            return obj.GetValue() != null;
        }

        public static bool HasValidValue(this IItem obj)
        {
            return obj.GetCheckRules().GetCheckResult(obj.GetValue());
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

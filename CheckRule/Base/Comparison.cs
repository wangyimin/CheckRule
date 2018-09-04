using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Base
{
    public interface IComparison
    {
        int Compare(object v1, object v2);
    }

    public class CommComparison : IComparison
    {
        public int Compare(object v1, object v2)
        {
            if (v1 == null || v2 == null)
            {
                throw new ArgumentNullException("Null parameter.");
            }

            if (v1.GetType() != v2.GetType())
            {
                throw new InvalidOperationException("Unable to comapre different type.");
            }

            if (v1.GetType() == typeof(int))
            {
                return ((int)v1).CompareTo((int)v2);
            }
            else if (v1.GetType() == typeof(long))
            {
                return ((long)v1).CompareTo((long)v2);
            }
            else if (v1.GetType() == typeof(DateTime))
            {
                return (((DateTime)v1).ToBinary()).CompareTo(((DateTime)v2).ToBinary());
            }
            else
            {
                throw new NotSupportedException("Unsupported compare type[" + v1.GetType().Name + "].");
            }
        }
    }

    public class CustComparison : IComparison
    {
        public int Compare(object v1, object v2)
        {
            if (v1 == null || v2 == null)
            {
                throw new ArgumentNullException("Null parameter.");
            }

            if (v1.GetType() != v2.GetType())
            {
                throw new InvalidOperationException("Unable to comapre different type.");
            }

            if (v1.GetType() == typeof(int))
            {
                return ((int)v1).CompareTo((int)v2);
            }
            else if (v1.GetType() == typeof(long))
            {
                return ((long)v1).CompareTo((long)v2);
            }
            else if (v1.GetType() == typeof(DateTime))
            {
                return (((DateTime)v1).ToBinary()).CompareTo(((DateTime)v2).ToBinary());
            }
            else
            {
                throw new NotSupportedException("Unsupported compare type[" + v1.GetType().Name + "].");
            }
        }
    }
}

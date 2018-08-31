using Base;
using Demo;
using System.Diagnostics;

namespace ConsoleApp2
{
    class Program
    {
        static void Main(string[] args)
        {
            Dec dec = new Dec(12);

            CheckRules checks = new CheckRules();

            CheckRules subc = new CheckRules();
            subc.Add(Operator.NONE, new CheckRule(Operator.LT, 12));

            CheckRules subcc = new CheckRules();
            subcc.Add(Operator.NONE, new CheckRule(Operator.GT, 10));

            dec.SetCheckRules(
                checks
                    .Add(Operator.NONE, new CheckRule(Operator.LT, 13))
                    .Add(Operator.AND,  new CheckRule(Operator.GT, 11))
                    .Add(Operator.OR, subc.Add(Operator.AND, subcc))
                );

            Trace.WriteLine(dec.IsNotNull());
            Trace.WriteLine(dec.HasValidValue());
   
            Trace.WriteLine("End");
        }
    }
}

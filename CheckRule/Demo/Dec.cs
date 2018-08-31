using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Base;

namespace Demo
{
    class Dec : IItem
    {
        private int _value;
        private CheckRules _checks;

        public Dec(int value)
        {
            _value = value;
        }

        public object GetValue()
        {
            return _value;
        }

        public void SetCheckRules(CheckRules checks)
        {
            _checks = checks;
        }

        public CheckRules GetCheckRules()
        {
            return _checks;
        }
    }
}

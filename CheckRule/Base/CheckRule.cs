namespace Base
{
    public class CheckRule
    {
        private Operator _operator;
        private object _value;

        public CheckRule(Operator _operator, object _value)
        {
            this._operator = _operator;
            this._value = _value;
        }

        public override string ToString()
        {
            return _operator.Display() 
                + (_value is IItem ? this.Convert(((IItem)_value).GetValue()) : this.Convert(_value));
        }

    }
}

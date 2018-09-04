namespace Base
{
    public enum Operator
    {
        [OperatorAttributes(Display = "==")]
        EQ,
        [OperatorAttributes(Display = "<>")]
        NE,
        [OperatorAttributes(Display = ">", Comparison = typeof(CommComparison))]
        GT,
        [OperatorAttributes(Display = ">=")]
        GE,
        [OperatorAttributes(Display = "<", Comparison = typeof(CustComparison))]
        LT,
        [OperatorAttributes(Display = "<=")]
        LE,
        [OperatorAttributes(Display = "in")]
        IN,
        [OperatorAttributes(Display = "&&")]
        AND,
        [OperatorAttributes(Display = "||")]
        OR,
        [OperatorAttributes(Display = "")]
        NONE
    }
}

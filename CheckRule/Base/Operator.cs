namespace Base
{
    public enum Operator
    {
        [OperatorAttributes(Display = "==")]
        EQ,
        [OperatorAttributes(Display = "<>")]
        NE,
        [OperatorAttributes(Display = ">")]
        GT,
        [OperatorAttributes(Display = ">=")]
        GE,
        [OperatorAttributes(Display = "<")]
        LT,
        [OperatorAttributes(Display = "<=")]
        LE,
        [OperatorAttributes(Display = "in", Comparison = typeof(InComparison))]
        IN,
        [OperatorAttributes(Display = "&&")]
        AND,
        [OperatorAttributes(Display = "||")]
        OR,
        [OperatorAttributes(Display = "")]
        NONE
    }
}

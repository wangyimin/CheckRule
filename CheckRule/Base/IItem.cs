namespace Base
{
    public interface IItem
    {
        object GetValue();
        void SetCheckRules(CheckRules checks);
        CheckRules GetCheckRules();
    }
}

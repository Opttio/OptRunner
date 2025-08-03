namespace _OprRunner.Scripts.Core.ConditionsActionsExecutors
{
    public class AndCondition : ICondition
    {
        private ICondition[] _conditions;

        public AndCondition(params ICondition[] conditions)
        {
            _conditions = conditions;
        }
        
        public bool IsMet()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.IsMet())
                    return false;
            }
            return true;
        }
    }
}
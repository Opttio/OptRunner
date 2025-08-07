namespace _OprRunner.Scripts.Core.ConditionsActionsExecutors
{
    public class OneShotCondition : ICondition
    {
        private readonly ICondition _innerCondition;
        private bool _hasFired;

        public OneShotCondition(ICondition innerCondition)
        {
            _innerCondition = innerCondition;
            _hasFired = false;
        }

        public void Reset() => _hasFired = false;

        public bool IsMet()
        {
            if (_hasFired)
                return false;
            if (_innerCondition.IsMet())
            {
                _hasFired = true;
                return true;
            }
            return false;
        }
    }
}
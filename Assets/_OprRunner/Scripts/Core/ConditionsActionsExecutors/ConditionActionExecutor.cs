using System.Collections.Generic;

namespace _OprRunner.Scripts.Core.ConditionsActionsExecutors
{
    public class ConditionActionExecutor
    {
        private readonly List<(ICondition condition, IAction action)> _rules = new();

        public void AddRule(ICondition condition, IAction action)
        {
            _rules.Add((condition, action));
        }
        
        public void RemoveRule(ICondition condition, IAction action)
        {
            _rules.Remove((condition, action));
        }

        public void ClearRules()
        {
            _rules.Clear();
        }

        public void Execute()
        {
            foreach (var rule in _rules)
                if (rule.condition.IsMet())
                {
                    rule.action.Execute();
                }
        }
    }
}
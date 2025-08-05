using _OprRunner.Scripts.Core.ConditionsActionsExecutors;

namespace _OprRunner.Scripts.Character
{
    public class RollInputCondition : ICondition
    {
        private bool _rollPressed;
        private IsGroundCondition _isGroundCondition;

        public RollInputCondition(PlayerInputAction inputAction,  IsGroundCondition isGroundCondition)
        {
            _isGroundCondition = isGroundCondition;
            inputAction.PlayerMap.Roll.performed += ctx =>
            {
                if (_isGroundCondition.IsMet())
                    _rollPressed = true;
            };
        }

        public bool IsMet()
        {
            if (_rollPressed)
            {
                _rollPressed = false;
                return true;
            } 
            return false;
        }
    }
}
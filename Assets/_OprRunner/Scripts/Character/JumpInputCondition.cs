using _OprRunner.Scripts.Core.ConditionsActionsExecutors;

namespace _OprRunner.Scripts.Character
{
    public class JumpInputCondition : ICondition
    {
        private bool _jumpPressed;
        private IsGroundCondition _isGroundCondition;

        public JumpInputCondition(PlayerInputAction inputAction, IsGroundCondition isGroundCondition)
        {
            _isGroundCondition = isGroundCondition;
            
            inputAction.PlayerMap.Jump.performed += ctx =>
            {
                if (_isGroundCondition.IsMet())
                    _jumpPressed = true;
            };
        }

        public bool IsMet()
        {
            if (_jumpPressed)
            {
                _jumpPressed = false;
                return true;
            }
            return false;
        }
    }
}
using _OprRunner.Scripts.Core.ConditionsActionsExecutors;

namespace _OprRunner.Scripts.Character
{
    public class JumpInputCondition : ICondition
    {
        private bool _jumpPressed;

        public JumpInputCondition(PlayerInputAction inputAction)
        {
            inputAction.PlayerMap.Jump.performed += ctx => _jumpPressed = true;
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
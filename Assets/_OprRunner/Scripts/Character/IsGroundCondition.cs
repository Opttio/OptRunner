using _OprRunner.Scripts.Core.ConditionsActionsExecutors;
using UnityEngine;

namespace _OprRunner.Scripts.Character
{
    public class IsGroundCondition : ICondition
    {
        private Transform _characterRunnerTransform;
        private float _groundCheckDistance;

        public IsGroundCondition(Transform characterRunnerTransform, float groundCheckDistance)
        {
            _characterRunnerTransform = characterRunnerTransform;
            _groundCheckDistance = groundCheckDistance;
        }
        public bool IsMet()
        {
            Vector3 gapPosition = _characterRunnerTransform.transform.position + Vector3.up * 0.1f;
            if (Physics.Raycast(gapPosition, Vector3.down, _groundCheckDistance))
                return true;
            else
                return false;
        }
    }
}
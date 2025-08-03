using _OprRunner.Scripts.Core.ConditionsActionsExecutors;
using UnityEngine;
using System.Collections;

namespace _OprRunner.Scripts.Character
{
    public class JumpAction : IAction
    {
        private Transform _character;
        private Transform _jumpUpPosition;
        private float _jumpDuration;
        private Coroutine _jumpRoutine = null;
        private CharacterRunnerAnimation _characterRunnerAnimation;
        private MonoBehaviour _monoBehaviour;

        public JumpAction(Transform character, Transform jumpUpPosition, float jumpDuration,
            CharacterRunnerAnimation characterRunnerAnimation, MonoBehaviour monoBehaviour)
            {
                _character = character;
                _jumpUpPosition = jumpUpPosition;
                _jumpDuration = jumpDuration;
                _characterRunnerAnimation = characterRunnerAnimation;
                _monoBehaviour = monoBehaviour;
            }
        private void Jump()
        {
            if (!_jumpUpPosition)
            {
                Debug.LogWarning("Позиція для стрибку не визначена!");
                return;
            }
            if (_jumpRoutine != null)
                _monoBehaviour.StopCoroutine(_jumpRoutine);
            Vector3 targetPosition = new Vector3(_character.position.x, _jumpUpPosition.position.y, _character.position.z);
            _jumpRoutine = _monoBehaviour.StartCoroutine(SmoothJump(targetPosition, _jumpDuration));
        }

        private IEnumerator SmoothJump(Vector3 targetPosition, float jumpDuration)
        {
            Vector3 startPosition = _character.position;
            float elapsed = 0f;
            while (elapsed < jumpDuration)
            {
                elapsed += Time.deltaTime;
                float lerpValue = elapsed / jumpDuration;
                float newY = Mathf.Lerp(startPosition.y, targetPosition.y, lerpValue);
                _character.position = new Vector3(_character.position.x, newY, _character.position.z);
                yield return null;
            }
            _character.position = new Vector3(_character.position.x, targetPosition.y, _character.position.z);
            elapsed = 0f;
            while (elapsed < jumpDuration)
            {
                elapsed += Time.deltaTime;
                float lerpValue = elapsed / jumpDuration;
                float newY = Mathf.Lerp(targetPosition.y, startPosition.y, lerpValue);
                _character.position = new Vector3(_character.position.x, newY, _character.position.z);
                yield return null;
            }
            _character.position = new Vector3(_character.position.x, startPosition.y, _character.position.z);
            _jumpRoutine = null;
        }
        public void Execute()
        {
            Jump();
            _characterRunnerAnimation.TriggerJumpAnimation();
            Debug.Log("JumpAction: виконано стрибок");
        }
    }
}
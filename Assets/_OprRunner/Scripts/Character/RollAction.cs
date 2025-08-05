using System.Collections;
using _OprRunner.Scripts.Core.ConditionsActionsExecutors;
using UnityEngine;

namespace _OprRunner.Scripts.Character
{
    public class RollAction : IAction
    {
        private float _rollDuration;
        private Collider _characterHighCollider;
        private Collider _characterLowCollider;
        private Coroutine _rollRoutine = null;
        private CharacterRunnerAnimation _characterRunnerAnimation;
        private MonoBehaviour _monoBehaviour;

        public RollAction(float rollDuration, Collider characterHighCollider, Collider characterLowCollider, 
            CharacterRunnerAnimation characterRunnerAnimation, MonoBehaviour monoBehaviour)
        {
            _rollDuration = rollDuration;
            _characterHighCollider = characterHighCollider;
            _characterLowCollider = characterLowCollider;
            _characterRunnerAnimation = characterRunnerAnimation;
            _monoBehaviour = monoBehaviour;
        }

        private void Roll()
        {
            if (_rollRoutine != null) 
                _monoBehaviour.StopCoroutine(_rollRoutine);
            OffAllColliders();
            OnLowCollider();
            _rollRoutine = _monoBehaviour.StartCoroutine(RollCoroutine());
            
        }

        private IEnumerator RollCoroutine()
        {
            float elapsed = 0f;
            while (elapsed < _rollDuration)
            {
                elapsed += Time.deltaTime;
                yield return null;
            }
            OffAllColliders();
            OnHighCollider();
        }

        private void OffAllColliders()
        {
            if (_characterHighCollider)
                _characterHighCollider.enabled = false;

            if (_characterLowCollider)
                _characterLowCollider.enabled = false;
        }

        private void OnLowCollider()
        {
            _characterLowCollider.enabled = true;
        }

        private void OnHighCollider()
        {
            _characterHighCollider.enabled = true;
        }

        public void Execute()
        {
            Roll();
            _characterRunnerAnimation.TriggerRollAnimation();
        }
    }
}
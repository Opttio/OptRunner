using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _OprRunner.Scripts.Character
{
    public class CharacterRunnerController : MonoBehaviour
    {
        [Header("General")]
        [SerializeField] private GameObject _road;
        [Header("Movement")]
        [SerializeField] private float _leftRightDuration;
        [Header("Jump")]
        [SerializeField] private float _groundCheckDistance = 0.2f;
        [SerializeField] private float _jumpDuration = 1f;
        [SerializeField] private Transform _jumpUpPosition;
        [Header("Animation")]
        [SerializeField] private CharacterRunnerAnimation _characterRunnerAnimation;
        
        private PlayerInputAction _playerInputAction;
        private Transform[] _stripes;
        private int _currentStripeIndex = 1;
        private Coroutine _smoothMoveRoutine;
        private Coroutine _jumpRoutine;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.PlayerMap.ToLeft.performed += ToLeft;
            _playerInputAction.PlayerMap.ToRight.performed += ToRight;
            _playerInputAction.PlayerMap.Jump.performed += OnJump;
            
            AssignStripes();
        }

        private void OnEnable()
        {
            _playerInputAction.Enable();
        }
        private void OnDisable()
        {
            _playerInputAction.Disable();
        }

        private void AssignStripes()
        {
            if (!_road)
            {
                Debug.LogWarning("Road GameObject не призначено в інспекторі!");
                return;
            }

            Transform stripesHolder = _road.transform.Find("StripesHolder");

            if (stripesHolder)
            {
                _stripes = new Transform[3];
                _stripes[0] = stripesHolder.Find("LeftStripe");
                _stripes[1] = stripesHolder.Find("MiddleStripe");
                _stripes[2] = stripesHolder.Find("RightStripe");

                for (int i = 0; i < _stripes.Length; i++)
                {
                    if (!_stripes[i])
                        Debug.LogWarning($"Stripe {i} не знайдено.");
                }
            }
            else
            {
                Debug.LogWarning("StripesHolder не знайдено всередині Road.");
            }
        }

        private void MoveToStripe(int index)
        {
            if (_stripes == null || _stripes.Length == 0 || !_stripes[index])
            {
                Debug.LogWarning("Смуга не визначена!");
                return;
            }
            Vector3 targetPosition = new Vector3(_stripes[index].position.x, transform.position.y, transform.position.z);
            if (_smoothMoveRoutine != null)
                StopCoroutine(_smoothMoveRoutine);
            _smoothMoveRoutine = StartCoroutine(SmoothMove(targetPosition, _leftRightDuration));
            
            
        }

        private IEnumerator SmoothMove(Vector3 targetPosition, float baseDuration)
        {
            Vector3 startPosition = transform.position;
            float distance = Vector3.Distance(startPosition, targetPosition);
            float fullDistance = Vector3.Distance(_stripes[0].position, _stripes[2].position);
            float adjustedDuration = baseDuration * (distance / fullDistance);  
            float elapsed = 0f;
            while (elapsed < adjustedDuration)
            {
                elapsed += Time.deltaTime;
                float lerpValue = elapsed / adjustedDuration;
                float newX = Mathf.Lerp(startPosition.x, targetPosition.x, lerpValue);
                transform.position = new Vector3(newX, transform.position.y, transform.position.z);
                yield return null;
            }
            transform.position = new Vector3(targetPosition.x, transform.position.y, transform.position.z);
            _smoothMoveRoutine = null;
        }

        public bool _isGrounded()
        {
            Vector3 gapPosition = transform.position + Vector3.up * 0.1f;
            if (Physics.Raycast(gapPosition, Vector3.down, _groundCheckDistance))
                return true;
            else
                return false;
        }

        private void Jump()
        {
            if (!_jumpUpPosition)
            {
                Debug.LogWarning("Позиція для стрибку не визначена!");
                return;
            }
            if (_jumpRoutine != null)
                StopCoroutine(_jumpRoutine);
            Vector3 targetPosition = new Vector3(transform.position.x, _jumpUpPosition.position.y, transform.position.z);
            _jumpRoutine = StartCoroutine(SmoothJump(targetPosition, _jumpDuration));
        }

        private IEnumerator SmoothJump(Vector3 targetPosition, float jumpDuration)
        {
            Vector3 startPosition = transform.position;
            float elapsed = 0f;
            while (elapsed < jumpDuration)
            {
                elapsed += Time.deltaTime;
                float lerpValue = elapsed / jumpDuration;
                float newY = Mathf.Lerp(startPosition.y, targetPosition.y, lerpValue);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, targetPosition.y, transform.position.z);
            elapsed = 0f;
            while (elapsed < jumpDuration)
            {
                elapsed += Time.deltaTime;
                float lerpValue = elapsed / jumpDuration;
                float newY = Mathf.Lerp(targetPosition.y, startPosition.y, lerpValue);
                transform.position = new Vector3(transform.position.x, newY, transform.position.z);
                yield return null;
            }
            transform.position = new Vector3(transform.position.x, startPosition.y, transform.position.z);
            _jumpRoutine = null;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                Debug.Log("Персонаж увійшов у тригер перешкоди");
            }
        }

        private void ToLeft(InputAction.CallbackContext obj)
        {
            if (_currentStripeIndex > 0)
            {
                _currentStripeIndex--;
                MoveToStripe(_currentStripeIndex);
            }
        }

        private void ToRight(InputAction.CallbackContext obj)
        {
            if (_currentStripeIndex < _stripes.Length - 1)
            {
                _currentStripeIndex++;
                MoveToStripe(_currentStripeIndex);
            }
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (_isGrounded())
            {
                Jump();
                _characterRunnerAnimation.TriggerJumpAnimation();
            }
        }
    }
}
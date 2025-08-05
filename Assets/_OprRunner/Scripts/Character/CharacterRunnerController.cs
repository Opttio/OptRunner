using System.Collections;
using _OprRunner.Scripts.Core.ConditionsActionsExecutors;
using _OprRunner.Scripts.Core.EventBus;
using _OprRunner.Scripts.Environment.Obstacles;
using _OprRunner.Scripts.UI.Models;
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
        [Header("Roll")]
        [SerializeField] private float _rollDuration;
        [SerializeField] private Collider _characterHighCollider;
        [SerializeField] private Collider _characterLowCollider;
        [Header("Animation")]
        [SerializeField] private CharacterRunnerAnimation _characterRunnerAnimation;
        
        public float GroundCheckDistance => _groundCheckDistance;
        
        private PlayerInputAction _playerInputAction;
        private Transform[] _stripes;
        private int _currentStripeIndex = 1;
        private Coroutine _smoothMoveRoutine;
        private Coroutine _jumpRoutine;
        private readonly int _startAttempts = 3;

        private ConditionActionExecutor _executor;

        private void Awake()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.PlayerMap.ToLeft.performed += ToLeft;
            _playerInputAction.PlayerMap.ToRight.performed += ToRight;
            
            AssignStripes();
        }

        private void Start()
        {
            _executor = new ConditionActionExecutor();
            var jumpCondition = new AndCondition(new IsGroundCondition(transform, _groundCheckDistance),
                new JumpInputCondition(_playerInputAction, new IsGroundCondition(transform, _groundCheckDistance)));
            var jumpAction = new JumpAction(transform, _jumpUpPosition, _jumpDuration, _characterRunnerAnimation, this);
            var rollCondition = new AndCondition(new IsGroundCondition(transform, _groundCheckDistance),
                new RollInputCondition(_playerInputAction, new IsGroundCondition(transform, _groundCheckDistance)));
            var rollAction = new RollAction(_rollDuration, _characterHighCollider,_characterLowCollider, _characterRunnerAnimation, this);
            _executor.AddRule(jumpCondition, jumpAction);
            _executor.AddRule(rollCondition, rollAction);
            
            GameModels.Attempt = _startAttempts;
            GameEventBus.ChangeAttempt(GameModels.Attempt);
        }

        private void Update()
        {
            _executor.Execute();
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

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Obstacle"))
            {
                GameModels.ViewId = 2;
                GameEventBus.ChangeView(GameModels.ViewId);
                GameModels.IsPlaying = false;
                GameEventBus.SwitchGameStatus(GameModels.IsPlaying);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                
                var obstacle = other.GetComponentInParent<ObstaclesDestroyer>();
                if (obstacle) 
                    Destroy(obstacle.gameObject);
                else
                    Debug.LogWarning("Obstacle hit, але не знайдено ObstaclesDestroyer!");
            }

            if (other.CompareTag("Coin"))
            {
                SignalToChangeCoin();
                Destroy(other.gameObject);
            }
        }

        private void SignalToChangeCoin()
        {
            GameModels.AddCoin(1);
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
    }
}
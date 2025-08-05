using UnityEngine;

namespace _OprRunner.Scripts.Character
{
    public class CharacterRunnerAnimation : MonoBehaviour
    {
        [SerializeField] private CharacterRunnerController _characterRunnerController;
        private Animator _animator;
        private IsGroundCondition _isGroundCondition;
        private static readonly int JumpStartTrigger =  Animator.StringToHash("JumpStart");
        private static readonly int IsGroundedBool = Animator.StringToHash("isGrounded");
        private static readonly int RollStartTrigger = Animator.StringToHash("RollStart");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Start()
        {
            _isGroundCondition = new IsGroundCondition(_characterRunnerController.transform, _characterRunnerController.GroundCheckDistance);
        }

        private void Update()
        {
            PlayLandAnimation();
        }

        private void PlayLandAnimation()
        {
            _animator.SetBool(IsGroundedBool, _isGroundCondition.IsMet());
        }
        
        public void TriggerJumpAnimation()
        {
            _animator.SetTrigger(JumpStartTrigger);
        }
        
        public void TriggerRollAnimation()
        {
            _animator.SetTrigger(RollStartTrigger);
        }
    }
}
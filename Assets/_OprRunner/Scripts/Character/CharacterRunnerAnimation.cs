using UnityEngine;

namespace _OprRunner.Scripts.Character
{
    public class CharacterRunnerAnimation : MonoBehaviour
    {
        [SerializeField] private CharacterRunnerController _characterRunnerController;
        private Animator _animator;
        private static readonly int JumpStartTrigger =  Animator.StringToHash("JumpStart");
        private static readonly int IsGroundedBool = Animator.StringToHash("isGrounded");
        
        private void Awake()
        {
            _animator = GetComponent<Animator>();
        }

        private void Update()
        {
            PlayLandAnimation();
        }

        private void PlayLandAnimation()
        {
            _animator.SetBool(IsGroundedBool, _characterRunnerController._isGrounded());
        }
        
        public void TriggerJumpAnimation()
        {
            _animator.SetTrigger(JumpStartTrigger);
        }
    }
}
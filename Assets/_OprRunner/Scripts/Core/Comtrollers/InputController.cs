using UnityEngine.InputSystem;

namespace _OprRunner.Scripts.Core.Comtrollers

{
    public static class InputController
    {
        private static PlayerInputAction _playerInputAction;

        static InputController()
        {
            _playerInputAction = new PlayerInputAction();
            _playerInputAction.UI.Enable();
        }
        
        public static InputAction PauseAction => _playerInputAction.UI.Pause;

        public static void EnableUI() => _playerInputAction.UI.Enable();
        public static void DisableUI() => _playerInputAction.UI.Disable();

        public static void EnableGameplay() => _playerInputAction.PlayerMap.Enable();
        public static void DisableGameplay() => _playerInputAction.PlayerMap.Disable();
    }
}
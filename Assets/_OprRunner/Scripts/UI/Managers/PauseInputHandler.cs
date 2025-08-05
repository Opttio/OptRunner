using _OprRunner.Scripts.Core.Comtrollers;
using _OprRunner.Scripts.Core.EventBus;
using _OprRunner.Scripts.UI.Models;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _OprRunner.Scripts.UI.Managers
{
    public class PauseInputHandler : MonoBehaviour
    {
        private void OnEnable()
        {
            InputController.EnableUI();
            InputController.PauseAction.performed += OnPausePerformed;
        }

        private void OnDisable()
        {
            InputController.DisableUI();
            InputController.PauseAction.performed -= OnPausePerformed;
        }

        private void OnPausePerformed(InputAction.CallbackContext obj)
        {
            GameModels.ViewId = 3;
            GameEventBus.ChangeView(GameModels.ViewId);
            GameModels.IsPlaying = false;
            GameEventBus.SwitchGameStatus(GameModels.IsPlaying);
            InputController.DisableGameplay();
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
}
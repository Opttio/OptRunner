using _OprRunner.Scripts.Core.Comtrollers;
using _OprRunner.Scripts.Core.EventBus;
using _OprRunner.Scripts.UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace _OprRunner.Scripts.UI.Views
{
    public class PauseView : MonoBehaviour
    {
        
        [SerializeField] private Button _continueButton;
        [SerializeField] private Button _resetButton;
        [SerializeField] private Button _exitButton;
        
        private void OnEnable() => SubscribeToEvents();

        private void OnDisable() => UnsubscribeFromEvents();

        private void SubscribeToEvents()
        {
            _continueButton.onClick.AddListener(SignalContinueGame);
            _resetButton.onClick.AddListener(SignalRestartGame);
            _exitButton.onClick.AddListener(SignalExitGame);
        }

        private void UnsubscribeFromEvents()
        {
            _continueButton.onClick.RemoveListener(SignalContinueGame);
            _resetButton.onClick.RemoveListener(SignalRestartGame);
        }

        private void SignalContinueGame()
        {
            GameModels.ViewId = 1;
            GameEventBus.ChangeView(GameModels.ViewId);
            GameModels.IsPlaying = true;
            GameEventBus.SwitchGameStatus(GameModels.IsPlaying);
            InputController.EnableGameplay();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void SignalRestartGame() => GameEventBus.TriggerRestart();
        private void SignalExitGame() => GameEventBus.TriggerExit();
    }
}
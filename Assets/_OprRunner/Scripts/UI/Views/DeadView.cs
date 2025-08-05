using _OprRunner.Scripts.Core.EventBus;
using _OprRunner.Scripts.UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace _OprRunner.Scripts.UI.Views
{
    public class DeadView : MonoBehaviour
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
            if (GameModels.Attempt == 0)
                return;
            GameModels.ViewId = 1;
            GameEventBus.ChangeView(GameModels.ViewId);
            GameModels.Attempt--;
            GameEventBus.ChangeAttempt(GameModels.Attempt);
            GameModels.IsPlaying = true;
            GameEventBus.SwitchGameStatus(GameModels.IsPlaying);
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void SignalRestartGame() => GameEventBus.TriggerRestart();
        private void SignalExitGame() => GameEventBus.TriggerExit();
    }
}
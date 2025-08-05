using _OprRunner.Scripts.Core.EventBus;
using _OprRunner.Scripts.UI.Models;
using UnityEngine;
using UnityEngine.UI;

namespace _OprRunner.Scripts.UI.Views
{
    public class StartView : MonoBehaviour
    {
        [SerializeField] private Button _startButton;
        
        private void OnEnable()
        {
            SubscribeToEvents();
        }

        private void OnDisable()
        {
            UnsubscribeFromEvents(); 
        }

        private void SubscribeToEvents()
        {
            _startButton.onClick.AddListener(SignalToStartGame);
        }

        private void UnsubscribeFromEvents()
        {
            _startButton.onClick.RemoveListener(SignalToStartGame);
        }

        private void SignalToStartGame()
        {
            GameModels.ViewId = 1;
            GameEventBus.ChangeView(GameModels.ViewId);
            GameModels.IsStarted = true;
            GameEventBus.TriggerGameStart(GameModels.IsStarted);
        }
    }
}
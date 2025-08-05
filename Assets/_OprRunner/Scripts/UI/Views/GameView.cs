using System;
using _OprRunner.Scripts.Core.EventBus;
using TMPro;
using UnityEngine;

namespace _OprRunner.Scripts.UI.Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _coinText;
        [SerializeField] private TextMeshProUGUI _attemptText;

        private void OnEnable() => SubscribeToEvents();
        private void OnDisable() => UnsubscribeFromEvents();

        private void SubscribeToEvents()
        {
            GameEventBus.OnCoinChanged += ChangeCoins;
            GameEventBus.OnAttemptChanged += ChangeAttempts;
        }

        private void UnsubscribeFromEvents()
        {
            GameEventBus.OnCoinChanged -= ChangeCoins;
            GameEventBus.OnAttemptChanged -= ChangeAttempts;
        }

        private void ChangeAttempts(int attempts)
        {
            _attemptText.text = attempts.ToString();
        }

        private void ChangeCoins(int coins)
        {
            _coinText.text = coins.ToString();
        }
    }
}
using System;

namespace _OprRunner.Scripts.Core.EventBus
{
    public static class GameEventBus
    {
        public static event Action<int> OnCoinChanged;
        public static event Action<int> OnAttemptChanged;
        public static event Action<int> OnViewChanged;
        public static event Action<bool> OnStarted;
        public static event Action<bool> OnGameStatus;
        public static event Action OnRestart;
        public static event Action OnExit;

        public static void ChangeCoin(int coin) => OnCoinChanged?.Invoke(coin);
        public static void ChangeAttempt(int attempt) => OnAttemptChanged?.Invoke(attempt);
        public static void ChangeView(int viewId) => OnViewChanged?.Invoke(viewId);
        public static void TriggerGameStart(bool isStarted) => OnStarted?.Invoke(isStarted);
        public static void SwitchGameStatus(bool isPlaying) => OnGameStatus?.Invoke(isPlaying);
        public static void TriggerRestart() => OnRestart?.Invoke();
        public static void TriggerExit() => OnExit?.Invoke();
    }
}
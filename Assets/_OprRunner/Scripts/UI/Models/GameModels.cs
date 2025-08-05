using _OprRunner.Scripts.Core.EventBus;

namespace _OprRunner.Scripts.UI.Models
{
    public static class GameModels
    {
        public static int Coin;
        public static int Attempt;
        public static int ViewId;
        public static bool IsStarted;
        public static bool IsPlaying;

        public static void AddCoin(int coin)
        {
            Coin += coin;
            if (Coin >= 100)
            {
                int extraLives = Coin / 100;
                Coin %= 100;
                Attempt += extraLives;
                GameEventBus.ChangeAttempt(Attempt);
            }
            GameEventBus.ChangeCoin(Coin);
        }
    }
}
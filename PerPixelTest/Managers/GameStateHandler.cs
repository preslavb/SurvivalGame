namespace PerPixelTest.Managers
{
    using PerPixelTest.Interfaces;

    public static class GameStateHandler
    {
        private static IGameState currentGameState;

        public static IGameState CurrentGameState
        {
            get
            {
                return currentGameState;
            }

            set
            {
                currentGameState = value;
            }
        }
    }
}

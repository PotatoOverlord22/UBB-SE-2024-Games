using GameWorld.Views;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Services;

namespace GameWorld.Utils
{
    internal class Router
    {
        private static Connect4GameGUI connectPage = new Connect4GameGUI();
        private static ObstructionGameGUI obstructionPage = new ObstructionGameGUI();
        private static ObstructionGameMode obstructionModePage = new ObstructionGameMode();
        private static MenuPage menuPage = new MenuPage();
        private static LoadingPage loadingPage = new LoadingPage();
        private static OpponentPage opponentPage = new OpponentPage();
        private static AIDifficultySelection aiSelectionPage = new AIDifficultySelection();
        private static string chessMode;
        private static int obstructionMode;
        private static string aiDifficulty;
        private static string gameType;
        private static bool onlineGame;
        private static Player userPlayer = new Player();
        private static Player opponentPlayer = new Player();
        private static IPlayService playService;
        public Router()
        {
        }
        public static string GameType
        {
            get { return gameType; }
            set { gameType = value; }
        }
        public static IPlayService PlayService
        {
            get { return playService; }
            set { playService = value; }
        }

        public static Connect4GameGUI ConnectPage
        {
            get { return connectPage; }
            set { connectPage = value; }
        }

        public static ObstructionGameGUI ObstructionPage
        {
            get { return obstructionPage; }
            set { obstructionPage = value; }
        }

        public static ObstructionGameMode ObstructionModePage
        {
            get { return obstructionModePage; }
            set { obstructionModePage = value; }
        }

        public static MenuPage MenuPage
        {
            get { return menuPage; }
            set { menuPage = value; }
        }

        public static LoadingPage LoadingPage
        {
            get { return loadingPage; }
            set { loadingPage = value; }
        }

        public static OpponentPage OpponentPage
        {
            get { return opponentPage; }
            set { opponentPage = value; }
        }

        public static AIDifficultySelection AiSelectionPage
        {
            get { return aiSelectionPage; }
            set { aiSelectionPage = value; }
        }

        public static string ChessMode
        {
            get { return chessMode; }
            set { chessMode = value; }
        }

        public static int ObstructionMode
        {
            get { return obstructionMode; }
            set { obstructionMode = value; }
        }

        public static string AiDifficulty
        {
            get { return aiDifficulty; }
            set { aiDifficulty = value; }
        }

        public static Player UserPlayer
        {
            get { return userPlayer; }
            set { userPlayer = value; }
        }

        public static Player OpponentPlayer
        {
            get { return opponentPlayer; }
            set { opponentPlayer = value; }
        }

        public static bool OnlineGame
        {
            get { return onlineGame; }
            set { onlineGame = value; }
        }
    }
}

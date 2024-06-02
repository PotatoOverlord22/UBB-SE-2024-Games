using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.Json;

namespace GameWorldClassLibrary.Services
{
    [Serializable]
    public class ObstructionGameService : IGame
    {
        private IBoard board;
        private GameState gameState;
        private int currentPlayer;

        public ObstructionGameService(Player player1, Player player2, int width, int heigth)
        {
            board = new ObstructionBoard(width, heigth);
            currentPlayer = 0;
            gameState = new GameState(player1, player2, GameStore.Games["Obstruction"]);
            SaveGame();
        }

        public ObstructionGameService(GameState unifinishedGameState)
        {
            board = new ObstructionBoard();
            gameState = unifinishedGameState;
            LoadGame();
        }

        [JsonIgnore]
        public IBoard Board { get => board; set => board = value; }
        [JsonIgnore]
        public GameState GameState { get => gameState; set => gameState = value; }

        [JsonIgnore]
        public Player CurrentPlayer => gameState.Players[currentPlayer];

        [JsonProperty]
        public ObstructionBoard JsonBoard { get => (ObstructionBoard)board; set => board = value; }

        [JsonProperty]
        public int CurrentPlayerIndex { get => currentPlayer; set => currentPlayer = value; }

        public string SaveGame()
        {
            string jsonString = JsonConvert.SerializeObject(this, Formatting.Indented);
            gameState.StateJson = jsonString;
            return jsonString;
        }

        public void LoadGame()
        {
            string json = gameState.StateJson;
            JObject obj = JsonConvert.DeserializeObject<JObject>(json);
            GetFromJObject(obj);
        }
        private void GetFromJObject(JObject obj)
        {
            board.GetFromJToken(obj["JsonBoard"]);
            currentPlayer = obj["CurrentPlayerIndex"].ToObject<int>();
        }
    }
}

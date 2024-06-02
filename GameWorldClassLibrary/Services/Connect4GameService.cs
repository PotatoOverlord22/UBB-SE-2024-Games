using System.Text.Json;
using GameWorldClassLibrary.Models;
using GameWorldClassLibrary.Repositories;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameWorldClassLibrary.Services
{
    public class Connect4GameService : IGame
    {
        private IBoard board;
        private GameState gameState;
        private int currentPlayer;

        public Connect4GameService(Player player1, Player player2)
        {
            board = new Connect4Board();
            currentPlayer = 0;
            gameState = new GameState(player1, player2, GameStore.Games["Connect4"]);
            SaveGame();
        }

        public Connect4GameService(GameState unifinishedGameState)
        {
            board = new Connect4Board();
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
        public Connect4Board JsonBoard { get => (Connect4Board)board; set => board = value; }

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

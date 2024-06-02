namespace GameWorldClassLibrary.Models
{
    public interface IGame
    {
        string SaveGame();
        void LoadGame();
        IBoard Board { get; set; }
        GameState GameState { get; set; }
        Player CurrentPlayer { get; }
        int CurrentPlayerIndex { get; }
    }
}

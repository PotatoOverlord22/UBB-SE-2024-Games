namespace GameWorldClassLibrary.Models
{
    public interface IPiece
    {
        int XPosition { get; set; }
        int YPosition { get; set; }
        Player? Player { get; set; }
    }
}

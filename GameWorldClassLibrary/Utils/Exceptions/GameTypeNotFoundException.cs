using System;

namespace TwoPlayerGames.exceptions
{
    public class GameTypeNotFoundException : Exception
    {
        public GameTypeNotFoundException(string message) : base(message)
        {
        }

        public GameTypeNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public GameTypeNotFoundException()
        {
        }
    }
}

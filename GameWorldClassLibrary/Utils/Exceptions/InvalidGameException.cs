using System;

namespace TwoPlayerGames.exceptions
{
    internal class InvalidGameException : Exception
    {
        public InvalidGameException(string message) : base(message)
        {
        }

        public InvalidGameException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidGameException()
        {
        }
    }
}

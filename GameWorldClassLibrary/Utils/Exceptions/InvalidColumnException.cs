using System;

namespace TwoPlayerGames.exceptions
{
    internal class InvalidColumnException : Exception
    {
        public InvalidColumnException(string message) : base(message)
        {
        }

        public InvalidColumnException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidColumnException()
        {
        }
    }
}

namespace GameWorldClassLibrary.exceptions
{
    public class InvalidColumnException : Exception
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

namespace GameWorldClassLibrary.exceptions
{
    public class InvalidDifficultyException : Exception
    {
        public InvalidDifficultyException(string message) : base(message)
        {
        }

        public InvalidDifficultyException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public InvalidDifficultyException()
        {
        }
    }
}

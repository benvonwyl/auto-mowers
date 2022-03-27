namespace auto_mowers.Services.MowerService.Exception
{
    public class InvalidMowerException : System.Exception
    {
        public InvalidMowerException(string? message) : base(message)
        {
        }

        public InvalidMowerException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}

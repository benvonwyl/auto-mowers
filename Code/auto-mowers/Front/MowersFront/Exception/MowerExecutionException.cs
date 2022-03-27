using System.Runtime.Serialization;

namespace auto_mowers.Front.AutoMowersFront.Exception
{
    public class MowerExecutionException : System.Exception
    {
        public MowerExecutionException(string? message) : base(message)
        {
        }

        public MowerExecutionException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}
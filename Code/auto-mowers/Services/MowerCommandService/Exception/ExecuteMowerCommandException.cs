using System.Runtime.Serialization;

namespace auto_mowers.Services.MowerCommandService.Exception
{
    [Serializable]
    public class ExecuteMowerCommandException : System.Exception
    {
        public ExecuteMowerCommandException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}
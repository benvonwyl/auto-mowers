using System.Runtime.Serialization;

namespace auto_mowers.Front.AutoMowersFront.Exception
{
    [Serializable]
    public class MowerDataFromFileLoadException : System.Exception
    {
        public MowerDataFromFileLoadException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}
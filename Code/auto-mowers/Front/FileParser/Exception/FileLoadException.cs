using System.Runtime.Serialization;

namespace auto_mowers.Front.FileParser.Exception
{
    [Serializable]
    public class FileLoadException : System.Exception
    {
        public FileLoadException(string? message) : base(message)
        {
        }

        public FileLoadException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}
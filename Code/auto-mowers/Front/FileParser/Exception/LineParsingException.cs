using System.Runtime.Serialization;

namespace auto_mowers.Front.FileParser.Exception
{
    public class LineParsingException : System.Exception
    {
        public LineParsingException(string? message) : base(message)
        {
        }

        public LineParsingException(string? message, System.Exception? innerException) : base(message, innerException)
        {
        }
    }
}

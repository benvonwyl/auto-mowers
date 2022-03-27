using System.Runtime.Serialization;

namespace auto_mowers.Services.MowerService.Exception
{
    [Serializable]
    public class MowerNotFoundException : System.Exception
    {
        public MowerNotFoundException(string? message) : base(message)
        {
        }
    }
}
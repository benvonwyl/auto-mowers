namespace auto_mowers.Services.LawnService.Exception
{
    public class InvalidLawnException : System.Exception
    {
        public InvalidLawnException(string name)
            : base(name)
        {

        }
    }
}

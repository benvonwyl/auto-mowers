namespace auto_mowers.Services.LawnService.Exception
{
    public class LawnNotfoundException : System.Exception
    {
        public LawnNotfoundException()
        {
        }
        public LawnNotfoundException(string name)
            : base(name)
        {

        }
    }
}

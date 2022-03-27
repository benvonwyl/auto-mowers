using auto_mowers.Services.MowerFront.Contract;

namespace auto_mowers.Front.AutoMowersFront.Contract
{
    public interface IMowersFront
    {
        public int Run(string[] args);
    }
}
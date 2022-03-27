namespace auto_mowers.Front.ArgumentParser.Contract
{
    public interface IArgumentParser
    {
        Arguments Parse(string[] args);
    }
}
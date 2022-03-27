using auto_mowers.Front.ArgumentParser.Contract;
using auto_mowers.Front.ArgumentParser.Exception;
using CommandLine;
using Microsoft.Extensions.Logging;

namespace auto_mowers.Front.ArgumentParser
{
    public class ArgumentParser : IArgumentParser
    {
        private readonly ILogger<ArgumentParser> _logger;

        public ArgumentParser(ILogger<ArgumentParser> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Parse arguments in input of mower program
        /// </summary>
        /// <param name="args"></param>
        /// <returns></returns>
        /// <exception cref="ProgramArgumentException"></exception>
        public Arguments Parse(string[] args) 
        {
            Arguments arguments = null;

            try
            {
                    Parser.Default.ParseArguments<Arguments>(args)
                    .WithParsed(result => arguments = result)
                    .WithNotParsed(err => throw new ProgramArgumentException(err.Count() + " errors in Command Line, arguments are not valid"));
            }
            catch (ArgumentNullException e)
            {
                throw new ProgramArgumentException("arguments furnished are null");
            }

            if (arguments == null) 
            {
                throw new ProgramArgumentException("results of parsed arguments is null");
            }
            
            return arguments;
        }
    }
}


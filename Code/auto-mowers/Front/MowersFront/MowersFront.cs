using auto_mowers.Front.ArgumentParser.Contract;
using auto_mowers.Front.ArgumentParser.Exception;
using auto_mowers.Front.AutoMowersFront.Contract;
using auto_mowers.Front.AutoMowersFront.Exception;
using auto_mowers.Front.FileParser.Contract;
using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerCommandService.Contract;
using auto_mowers.Services.MowerFront.Contract;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Request;
using Microsoft.Extensions.Logging;
using System.Text.RegularExpressions;

namespace auto_mowers.Front.AutoMowersFront
{
    public class MowersFront : IMowersFront
    {
        private readonly ILogger<MowersFront> _logger;
        private readonly IMowerService _mowerService;
        private readonly ILawnService _lawnService;
        private readonly IFileParser _fileParser;
        private readonly IMowerCommandService _mowerCommandService;
        private readonly IArgumentParser _argumentParser;

        public MowersFront(ILogger<MowersFront> logger,
            IMowerService mowerService,
            IFileParser fileParser,
            ILawnService lawnService,
            IMowerCommandService mowerCommandService,
            IArgumentParser argumentParser)
        {
            _logger = logger;
            _mowerService = mowerService;
            _fileParser = fileParser;
            _lawnService = lawnService;
            _mowerCommandService = mowerCommandService;
            _argumentParser = argumentParser;
        }

        /// <summary>
        /// Load an automower input file. 
        /// Record Lawn, Mowers and Commands.
        /// </summary>
        /// <param name="filePath"></param>
        /// <exception cref="auto_mowers.Front.AutoMowersFront.Exception.FileLoadException"></exception>
        /// <exception cref="LineParsingException"></exception>
        public List<MowerCommands> LoadFromFile(string filePath)
        {
            try
            {
                string[] lines;
                Guid lawnId;

                List<MowerCommands> mowerCommands = new List<MowerCommands>();

                lines = _fileParser.LoadFileFromPath(filePath);

                AddLawnRequest addLawnRequest = _fileParser.LoadLawnDefinitionLine(lines[0]);
                lawnId = _lawnService.AddLawn(addLawnRequest);

                for (int i = 1; i < lines.Length; i += 2)
                {

                    AddMowerRequest addMowerRequest = _fileParser.LoadMowerDefinitionLine(lines[i], lawnId);
                    Guid mowerId = _mowerService.AddMower(addMowerRequest);

                    MowerCommands parsedMowerCommands = _fileParser.LoadCommandsDefinitionLine(lines[i + 1], mowerId);

                    mowerCommands.Add(parsedMowerCommands);
                }

                return mowerCommands;
            }
            catch (System.Exception e)
            {
                throw new MowerDataFromFileLoadException("The Definition of mowers and instructions are incorrect : ", e);
            }
        }

        /// <summary>
        /// run the registered commands attached to mowerCommandsId in given order FIFO
        /// </summary>
        /// <param name="mowerCommandsId"></param>
        /// <exception cref="MowerExecutionException"></exception>
        public void RunAutoMowers(List<MowerCommands> mowerCommands)
        {
            if (mowerCommands == null || mowerCommands.Count == 0)
            {
                throw new MowerExecutionException("invalid command list");
            }

            try
            {
                foreach (var mowerCommand in mowerCommands)
                {
                    Position p = null;

                    foreach (MowerCommandType commandType in mowerCommand.Commands)
                    {
                        p = _mowerCommandService.ExecuteMowerCommand(mowerCommand.MowerId, commandType);
                    }

                    Console.WriteLine(p.X + " " + p.Y + " " + p.O);
                }
            }
            catch (System.Exception e)
            {
                throw new MowerExecutionException("Execution Error", e);
            }
        }
        /// <summary>
        /// run the registered commands attached to mowerCommandsId in given order FIFO
        /// </summary>
        /// <param name="mowerCommandsId"></param>
        /// <exception cref="MowerExecutionException"></exception>
        public int Run(string[] args)
        {
            try
            {
                Arguments arguments = _argumentParser.Parse(args);

                List<MowerCommands> commands = this.LoadFromFile(arguments.FilePath);

                this.RunAutoMowers(commands);
            }
            catch (ProgramArgumentException ex)
            {
                string message = "Error During Execution: Invalid Program Args Parsing. ";
                Console.WriteLine(message + ex.Message);
                _logger.LogError(ex, message);
                return 2;
            }
            catch (MowerDataFromFileLoadException ex)
            {
                string message = "Error During Execution: Invalid InputFile Parsing. ";
                Console.WriteLine(message + ex.Message);
                _logger.LogError(ex, message);
                return 2;
            }
            catch (MowerExecutionException ex)
            {
                string message = "Error During Execution: Invalid Executions of Mowers. ";
                Console.WriteLine(message);
                _logger.LogError(ex, message);
                return 2;
            }
            return 0;
        }
    }
}


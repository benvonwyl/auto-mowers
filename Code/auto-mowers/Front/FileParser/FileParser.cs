using auto_mowers.Front.FileParser.Contract;
using auto_mowers.Front.FileParser.Exception;
using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerFront.Contract;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Request;
using Microsoft.Extensions.Logging;
using System.IO.Abstractions;
using System.Text.RegularExpressions;

namespace auto_mowers.Front.FileParser
{
    public class FileParser : IFileParser
    {
        private readonly ILogger<FileParser> _logger;
        private readonly IFileSystem _fileSystem;

        private const int MinimumNbLines = 3;
        private const string LawnPattern = @"^(\d+)\ (\d+)$";

        private const string MowerPattern = @"^(\d+)\ (\d+)\ ([NEWS])$";
        private const string InstructionsPattern = @"^([LRF]+)$";


        public FileParser(ILogger<FileParser> logger, 
             IFileSystem fileSystem)
        {
            _logger = logger;
            _fileSystem = fileSystem;
        }

        /// <summary>
        /// Loads a file and split it into lines
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        /// <exception cref="Exception.FileLoadException"></exception>
        public string[] LoadFileFromPath(string path)
        {
            string[] lines = null;

            try
            {
                lines = _fileSystem.File.ReadAllLines(path);
            }
            catch (System.Exception e)
            {
                throw new Exception.FileLoadException("Cannot Open or Read the file : " + path ?? String.Empty, e);
            }

            if (lines.Length < MinimumNbLines || lines.Length % 2 != 1)
            {
                throw new Exception.FileLoadException("The file is uncomplete");
            }
            return lines;
        }

        /// <summary>
        /// Parse a Lawn Definition Line 
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        /// <exception cref="LineParsingException"></exception>
        public AddLawnRequest LoadLawnDefinitionLine(string line)
        {
            if (String.IsNullOrEmpty(line))
            {
                throw new LineParsingException("The Lawn line definition is incorrect");
            }

            int xLawn, yLawn;
            Regex lawnRegex;
            Match lawnMatch;

            try
            {
                lawnRegex = new Regex(LawnPattern);
                lawnMatch = lawnRegex.Match(line);

            }
            catch (System.Exception e)
            {
                throw new LineParsingException("The Lawn is not present or not formated correctly : ", e);
            }

            if (!lawnMatch.Success
                   || !int.TryParse(lawnMatch.Groups[1].Value, out xLawn)
                   || !int.TryParse(lawnMatch.Groups[2].Value, out yLawn))
            {
                throw new LineParsingException("The Lawn line definition is not present or not formated correctly");
            }

            return new AddLawnRequest { X = xLawn, Y = yLawn };
        }

        /// <summary>
        /// Parse a Mower Definitions Line  
        /// </summary>
        /// <param name="line"></param>
        /// <param name="lawnId"></param>
        /// <returns></returns>
        /// <exception cref="LineParsingException"></exception>
        public AddMowerRequest LoadMowerDefinitionLine(string line, Guid lawnId)
        {
            int x, y;
            char o;

            Regex mowerRegex;
            Match mowerMatch;

            if (String.IsNullOrEmpty(line))
            {
                throw new LineParsingException("The Lawn line definition is incorrect");
            }

            try
            {
                mowerRegex = new Regex(MowerPattern);
                mowerMatch = mowerRegex.Match(line);
            }
            catch (System.Exception e)
            {
                throw new LineParsingException("A mower line definition is not present or not formated correctly", e);
            }

            if (!mowerMatch.Success

                        || !int.TryParse(mowerMatch.Groups[1].Value, out x)
                        || !int.TryParse(mowerMatch.Groups[2].Value, out y)
                        || !char.TryParse(mowerMatch.Groups[3].Value, out o))
            {
                throw new LineParsingException("The Definition of mowers and instructions are incorrect");
            }

            return new AddMowerRequest { LawnId = lawnId, Position = new PositionRequest { X = x, Y = y, O = o }};
        }
        
        /// <summary>
        /// Parse a mowers commands line 
        /// </summary>
        /// <param name="line"></param>
        /// <param name="mowerId"></param>
        /// <returns></returns>
        /// <exception cref="LineParsingException"></exception>
        public MowerCommands LoadCommandsDefinitionLine(string line, Guid mowerId)
        {
            if (String.IsNullOrEmpty(line))
            {
                throw new LineParsingException("The Lawn line definition is incorrect");
            }

            Regex instructionsRegex;
            Match instructionsMatch;

            try
            {
                instructionsRegex = new Regex(InstructionsPattern);
                instructionsMatch = instructionsRegex.Match(line);
            }
            catch (System.Exception e)
            {
                throw new LineParsingException("A mower commands line definition is not present or not formated correctly", e);
            }

            if (!instructionsMatch.Success
                || String.IsNullOrEmpty(instructionsMatch.Groups[1].Value))
            {
                throw new LineParsingException("The Definition of mowers and instructions are incorrect");
            }

            List<MowerCommandType> commands = new List<MowerCommandType>();
            foreach (char c in instructionsMatch.Groups[1].Value)
            {
                MowerCommandType commandType;
                if (!MowerCommandType.TryParse(c.ToString(), out commandType))
                {
                    throw new LineParsingException("A given command is not valid");
                }

                commands.Add(commandType);
            }

            return new MowerCommands { MowerId = mowerId, Commands = commands };
        }
    }
}


using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerFront.Contract;
using auto_mowers.Services.MowerService.Request;

namespace auto_mowers.Front.FileParser.Contract
{
    public interface IFileParser
    {
        string[] LoadFileFromPath(string path);

        AddLawnRequest LoadLawnDefinitionLine(string line);
        AddMowerRequest LoadMowerDefinitionLine(string v, Guid lawnId);
        MowerCommands LoadCommandsDefinitionLine(string v, Guid mowerId);
    }
}
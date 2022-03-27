using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Request;

namespace auto_mowers.Services.MowerCommandService.Contract
{
    public interface IMowerCommandService
    {
        Position ExecuteMowerCommand(Guid mowerId, MowerCommandType mct);
    }
}
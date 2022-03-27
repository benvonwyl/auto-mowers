using auto_mowers.Services.MowerService.Request;

namespace auto_mowers.Services.MowerService.Contract
{
    public interface IMowerService
    {
        Guid AddMower(AddMowerRequest mowerRequest);

        Mower GetMower(Guid mowerId);
    }
}
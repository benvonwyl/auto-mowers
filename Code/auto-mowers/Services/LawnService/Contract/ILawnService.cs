using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerService.Request;

namespace auto_mowers.Services.LawnService.Contract
{
    public interface ILawnService
    {
        Guid AddLawn(AddLawnRequest request);
      
        Lawn GetLawnById(Guid id);
    }
}
using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.LawnService.Exception;
using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Exception;
using auto_mowers.Services.MowerService.Request;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;

namespace auto_mowers.Services.MowerService
{

    
    public class LawnService : ILawnService
    {
        private readonly ILogger<ILawnService> _logger;
        /// <summary>
        /// Cheap DAL to access Lawns
        /// </summary>
        public Dictionary<Guid, Lawn> Lawns { get; private set; } = new Dictionary<Guid, Lawn>();

        public LawnService(ILogger<ILawnService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Add Lawn somewhere 
        /// </summary>
        /// <param name="lawn"></param>
        /// <returns>id</returns>
        /// <exception cref="InvalidLawnException"></exception>
        public Guid AddLawn(AddLawnRequest lawn)
        {
            if (lawn == null || lawn.X < 0 || lawn.Y < 0)
            {
                throw new InvalidLawnException("size of the lawn is unvalid");
            }

            try
            {
                Guid key = Guid.NewGuid();
                Lawns.Add(key, new Lawn() { X = lawn.X , Y = lawn.Y});
                return key;
            }
            catch (System.Exception e)
            {
                throw new InvalidLawnException("stg went wrong");
            }
        }
               
        /// <summary>
        ///  Gets a Lawn by Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="LawnNotfoundException"></exception>
        public Lawn GetLawnById(Guid id)
        {
            if (!Lawns.ContainsKey(id))
            {
                throw new LawnNotfoundException("Lawn Not found ");
            }
            return Lawns[id];
        }
    }
}


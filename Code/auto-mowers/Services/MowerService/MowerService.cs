using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.LawnService.Exception;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Exception;
using auto_mowers.Services.MowerService.Request;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;

namespace auto_mowers.Services.MowerService
{
    /// <summary>
    /// TODO 
    /// code coverage : 90%
    /// unvalid examples 
    /// users manual 
    /// -- verbose 
    /// XunitDisplayName
    /// factoriser mock 
    /// SPEC + Shéma archi + shéma archi future +  Screen des tests + outil code coverage + détail qualité de code 
    /// comment Methods 
    /// REQUEST PARTOUT 
    /// corriger Exceptions et rethrow  voir comment ca se logge bien  
    /// integration github CI
    /// Integration test ? 
    /// </summary>
    public class MowerService : IMowerService
    {
        private readonly ILogger<IMowerService> _logger;
        private readonly ILawnService _lawnService;

        /// <summary>
        /// Cheap DAL to access Mowers 
        /// </summary>
        private List<Mower> Mowers { get;  set; } = new List<Mower>();

        public MowerService(ILogger<IMowerService> logger, ILawnService lawnService)
        {
            _logger = logger;
            _lawnService = lawnService;
        }

        /// <summary>
        /// Record a Mower somewhere
        /// </summary>
        /// <param name="mowerRequest"></param>
        /// <returns>mowerid</returns>
        /// <exception cref="InvalidMowerException"></exception>
        public Guid AddMower(AddMowerRequest mowerRequest)
        {
            if (mowerRequest == null || mowerRequest.Position == null)
            {
                throw new InvalidMowerException("Invalid Mower Request");
            }

            OrientationType orientation;
            Lawn lawn;

            try
            {
                lawn = _lawnService.GetLawnById(mowerRequest.LawnId);
            }
            catch (LawnNotfoundException ex)
            {
                throw new InvalidMowerException("Specified Lawn Does Not Exists", ex);
            }

            // coordinates of the mower are valid and in the lawn.  
            if (mowerRequest.Position.X < 0
                || mowerRequest.Position.Y < 0
                || lawn.X < mowerRequest.Position.X
                || lawn.Y < mowerRequest.Position.Y
                || !OrientationType.TryParse(mowerRequest.Position.O.ToString(), out orientation))
            {
                throw new InvalidMowerException("Initial Coordinates or orientations of the mower are not valid");
            }

            Guid guid = Guid.NewGuid();

            this.Mowers.Add(new Mower { Id = guid, P = new Position { X = mowerRequest.Position.X, Y = mowerRequest.Position.Y, O = orientation }, LawnId = mowerRequest.LawnId });

            return guid;
        }

        /// <summary>
        /// Get A Mower by its id  
        /// </summary>
        /// <param name="mowerId"></param>
        /// <returns></returns>
        /// <exception cref="MowerNotFoundException"></exception>
        public Mower GetMower(Guid mowerId)
        {
            if (!this.Mowers.Any(t => t.Id == mowerId))
            {
                throw new MowerNotFoundException("The Mower " + mowerId ?? string.Empty + " could not be found");
            }

            return this.Mowers.First(t => t.Id == mowerId);
        }
    }
}


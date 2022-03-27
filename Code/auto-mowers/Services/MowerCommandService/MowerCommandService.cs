using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.LawnService.Exception;
using auto_mowers.Services.MowerCommandService.Contract;
using auto_mowers.Services.MowerCommandService.Exception;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Exception;
using auto_mowers.Services.MowerService.Request;
using Microsoft.Extensions.Logging;
using System.Runtime.Serialization;

namespace auto_mowers.Services.MowerCommandService
{

    public class MowerCommandService : IMowerCommandService
    {
        private readonly ILogger<IMowerService> _logger;
        private readonly ILawnService _lawnService;
        private readonly IMowerService _mowerService;

        public MowerCommandService(ILogger<IMowerService> logger, ILawnService lawnService, IMowerService mowerService)
        {
            _logger = logger;
            _lawnService = lawnService;
            _mowerService = mowerService;
        }

        /// <summary>
        /// Execute a command on a mower 
        /// </summary>
        /// <param name="mowerId"></param>
        /// <param name="commandType"></param>
        /// <returns></returns>
        /// <exception cref="ExecuteMowerCommandException"></exception>
        public Position ExecuteMowerCommand(Guid mowerId, MowerCommandType command)
        {
            try
            {
                Mower mower = _mowerService.GetMower(mowerId);
                Lawn lawn = _lawnService.GetLawnById(mower.LawnId);

                switch (command)
                {
                    case MowerCommandType.L:
                        switch (mower.P.O)
                        {
                            case OrientationType.N:
                                mower.P.O = OrientationType.W;
                                break;
                            case OrientationType.E:
                                mower.P.O = OrientationType.N;
                                break;
                            case OrientationType.S:
                                mower.P.O = OrientationType.E;
                                break;
                            case OrientationType.W:
                                mower.P.O = OrientationType.S;
                                break;
                        }
                        break;
                    case MowerCommandType.R:
                        switch (mower.P.O)
                        {
                            case OrientationType.N:
                                mower.P.O = OrientationType.E;
                                break;
                            case OrientationType.E:
                                mower.P.O = OrientationType.S;
                                break;
                            case OrientationType.S:
                                mower.P.O = OrientationType.W;
                                break;
                            case OrientationType.W:
                                mower.P.O = OrientationType.N;
                                break;
                        }
                        break;
                    case MowerCommandType.F:
                        switch (mower.P.O)
                        {
                            case OrientationType.N:
                                if (mower.P.Y + 1 <= lawn.Y)
                                {
                                    mower.P.Y += 1;
                                }
                                break;
                            case OrientationType.E:
                                if (mower.P.X + 1 <= lawn.X)
                                {
                                    mower.P.X += 1;
                                }
                                break;
                            case OrientationType.S:
                                if (mower.P.Y - 1 >= 0)
                                {
                                    mower.P.Y -= 1;
                                }
                                break;
                            case OrientationType.W:
                                if (mower.P.X - 1 >= 0)
                                {
                                    mower.P.X -= 1;
                                }
                                break;
                        }
                        break;
                }

                return mower.P;
            }
            catch (System.Exception ex)
            {
                throw new ExecuteMowerCommandException("an error occured during the execution of commands for mower " + mowerId, ex);
            }
        }
    }
}


using auto_mowers.Services.MowerService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace auto_mowers.Services.MowerFront.Contract
{
    public class MowerCommands
    {
        public Guid MowerId { get; set; }

        public List<MowerCommandType> Commands { get; set; } = new List<MowerCommandType>();
    }
}

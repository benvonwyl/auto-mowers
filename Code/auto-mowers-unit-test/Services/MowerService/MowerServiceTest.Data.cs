using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace auto_mowers_unit_test.Services.MowerServiceTestData
{
    public class ValidMowerAddRequest : IEnumerable<object[]>
    {
        private static Guid lawnId = Guid.NewGuid();

        private readonly List<object[]> validMowerAddRequest = new List<object[]>()
            {
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 0, Y = 0 , O = 'N' } }, 0 , 0 , 'N', "LeftCorner N in 4X4" },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 3, Y = 3 , O = 'N' } }, 3 , 3 , 'N', "RightCorner N in 4X4" },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 0, Y = 0 , O = 'S' } }, 0 , 0 , 'S', "LeftCorner S in 4X4" },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 0, Y = 0 , O = 'E' } }, 0 , 0 , 'E', "LeftCorner E in 4X4" },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 0, Y = 0 , O = 'W' } }, 0 , 0 , 'W', "LeftCorner W in 4X4" },
            };

        public IEnumerator<object[]> GetEnumerator() => validMowerAddRequest.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InValidMowerAddRequest : IEnumerable<object[]>
    {
        private static Guid lawnId = Guid.NewGuid();

        private readonly List<object[]> inValidMowerAddRequest = new List<object[]>()
            {
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, null}, 
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest { LawnId = lawnId, Position = null } },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = Guid.Empty, Position = new PositionRequest { X = 0, Y = 0 , O = 'N' } } },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = -5, Y = 0 , O = 'N' } } },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 0, Y = -5 , O = 'N' } } },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 0, Y = 0 , O = 'A' } } },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 0, Y = 4 , O = 'N' } } },
                new object[] { new Lawn { X = 3, Y = 3 }, lawnId, new AddMowerRequest{ LawnId = lawnId, Position = new PositionRequest { X = 4, Y = 0 , O = 'N' } } },
            };

        public IEnumerator<object[]> GetEnumerator() => inValidMowerAddRequest.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
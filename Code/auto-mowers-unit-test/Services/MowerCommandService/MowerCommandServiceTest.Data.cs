using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace auto_mowers_unit_test.Services.MowerCommandServiceTestData
{
    public class ValidMowerCommand : IEnumerable<object[]>
    {
        private static Guid lawnId = Guid.NewGuid();
        private static Guid mowerId = Guid.NewGuid();
        private static Lawn basicLawn = new Lawn { X = 2, Y = 2 };

    private readonly List<object[]> validMowerCommandTestData = new List<object[]>()
            {
                new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.N  } },
                    mowerId,
                    MowerCommandType.L,
                    new Position { X = 0, Y = 0, O = OrientationType.W },
                    "rotate L from N"},
                 new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.W  } },
                    mowerId,
                    MowerCommandType.L,
                    new Position { X = 0, Y = 0, O = OrientationType.S },
                    "rotate L from W"},
                  new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.S  } },
                    mowerId,
                    MowerCommandType.L,
                    new Position { X = 0, Y = 0, O = OrientationType.E },
                    "rotate L from S"},
                   new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.E  } },
                    mowerId,
                    MowerCommandType.L,
                    new Position { X = 0, Y = 0, O = OrientationType.N },
                    "rotate L from E"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.N  } },
                    mowerId,
                    MowerCommandType.R,
                    new Position { X = 0, Y = 0, O = OrientationType.E },
                    "rotate R from N"},                    
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.E  } },
                    mowerId,
                    MowerCommandType.R,
                    new Position { X = 0, Y = 0, O = OrientationType.S },
                    "rotate R from E"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.S  } },
                    mowerId,
                    MowerCommandType.R,
                    new Position { X = 0, Y = 0, O = OrientationType.W },
                    "rotate R from S"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 0 , O = OrientationType.W  } },
                    mowerId,
                    MowerCommandType.R,
                    new Position { X = 0, Y = 0, O = OrientationType.N },
                    "rotate R from W"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 1, Y = 1 , O = OrientationType.N  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 1, Y = 2, O = OrientationType.N },
                    "Go F orientated N"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 1, Y = 1 , O = OrientationType.W  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 0, Y = 1, O = OrientationType.W },
                    "Go F orientated W"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 1, Y = 1 , O = OrientationType.S  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 1, Y = 0, O = OrientationType.S },
                    "Go F orientated S"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 1, Y = 1 , O = OrientationType.E  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 2, Y = 1, O = OrientationType.E },
                    "Go F orientated E"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 1, Y = 2 , O = OrientationType.N  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 1, Y = 2, O = OrientationType.N },
                    "Go F orientated N Limit"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 0, Y = 1 , O = OrientationType.W  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 0, Y = 1, O = OrientationType.W },
                    "Go F orientated W Limit"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 1, Y = 0 , O = OrientationType.S  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 1, Y = 0, O = OrientationType.S },
                    "Go F orientated S Limit"},
                    new object[] {
                    basicLawn,
                    lawnId,
                    new Mower {Id = mowerId, LawnId = lawnId , P = new Position { X = 2, Y = 1 , O = OrientationType.E  } },
                    mowerId,
                    MowerCommandType.F,
                    new Position { X = 2, Y = 1, O = OrientationType.E },
                    "Go F orientated E Limit"},
            };


        public IEnumerator<object[]> GetEnumerator() => validMowerCommandTestData.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
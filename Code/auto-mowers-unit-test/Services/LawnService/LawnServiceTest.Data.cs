using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Request;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace auto_mowers_unit_test.Services.LawnServiceTestData
{
    public class ValidLawnAddRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> validLawnAddRequest = new List<object[]>()
            {
                new object[] { new AddLawnRequest { X = 0, Y = 0}, 0 , 0 , "size: 1X1" },
                new object[] { new AddLawnRequest { X = 3, Y = 3}, 3 , 3 , "size: 4X4" },
                new object[] { new AddLawnRequest { X = 4, Y = 3}, 4 , 3 , "size: 5X4" },
                new object[] { new AddLawnRequest { X = int.MaxValue, Y = int.MaxValue }, int.MaxValue, int.MaxValue, "size:int.MaxValue+1 X int.MaxValue" },
            };

        public IEnumerator<object[]> GetEnumerator() => validLawnAddRequest.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }

    public class InValidLawnAddRequest : IEnumerable<object[]>
    {
        private readonly List<object[]> inValidLawnAddRequest = new List<object[]>()
            {   new object[] { null },
                new object[] { new AddLawnRequest { X = -5, Y = 0} },
                new object[] { new AddLawnRequest { X = 3, Y = -3} },
            };

        public IEnumerator<object[]> GetEnumerator() => inValidLawnAddRequest.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
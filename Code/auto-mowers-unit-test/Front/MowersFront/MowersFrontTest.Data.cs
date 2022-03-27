using auto_mowers.Services.MowerFront.Contract;
using System;
using System.Collections;
using System.Collections.Generic;

namespace auto_mowers_unit_test.Front.MowersFrontTestData
{
    public static class MowersFrontTestData
    {

        public class UnvalidRunMowersinput : IEnumerable<object[]>
        {
            private readonly List<object[]> unvalidRunMowersinput = new List<object[]>()
            {
                new object[] { null },
                new object[] { new List<MowerCommands>()},
            };

            public IEnumerator<object[]> GetEnumerator() => unvalidRunMowersinput.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
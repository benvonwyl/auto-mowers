using auto_mowers.Front.ArgumentParser;
using auto_mowers.Front.ArgumentParser.Exception;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace auto_mowers_unit_test.Front.ArgumentParserTest
{
    public static class ArgumentParserTestData
    {
        public const string validArg = "--filepath";

        public const string wrongArg = "--notFilePath";

        public const string anyPath = "c:/mypath.txt";

        public class UnValidInputs : IEnumerable<object[]>
        {
            private readonly List<object[]> UnValidArguments = new List<object[]>()
            {
                new object[] { new string[] { wrongArg, anyPath } },
                new object[] { new string[] { wrongArg } },
                new object[] { null },

            };

            public IEnumerator<object[]> GetEnumerator () => UnValidArguments.GetEnumerator ();

            IEnumerator IEnumerable.GetEnumerator () => GetEnumerator ();
        }
    }
}
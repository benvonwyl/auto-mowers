using auto_mowers.Services.MowerService.Contract;
using System;
using System.Collections;
using System.Collections.Generic;

namespace auto_mowers_unit_test.Front.FileParserTest
{
    public static class FileParserTestData
    {
        public static string ValidPath = "path";
        public static string[] ValidData = { "5 5", "1 2 N", "LFLFLFLFF", "3 3 E", "FFRFFRFRRF" };

        public class UnValidFileOutput : IEnumerable<object[]>
        {
            private readonly List<object[]> UnValidFileData = new List<object[]>()
            {
                new object[] { new string[] { "5 5", "1 2 N", "LFLFLFLFF", "3 3 E", "LFLFLFLFF", "3 3 E" } },
                new object[] { new string[] { "5 5", "1 2 N", "LFLFLFLFF", "3 3 E" } },
                new object[] { new string[] { "5 5", "1 2 N" } },
                new object[] { new string[] { "1 2 N", "LFLFLFLFF", "3 3 E", "FFRFFRFRRF" } }
            };

            public IEnumerator<object[]> GetEnumerator() => UnValidFileData.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ValidLawnLine : IEnumerable<object[]>
        {
            private readonly List<object[]> ValidLawnLines = new List<object[]>()
            {
                new object[] { "5 5", 5 , 5 },
                new object[] { "2 5", 2 , 5 },
                new object[] { "5 3", 5 , 3 },
            };

            public IEnumerator<object[]> GetEnumerator() => ValidLawnLines.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        public class UnValidLawnLine : IEnumerable<object[]>
        {
            private readonly List<object[]> unValidLawnLines = new List<object[]>()
            {
                new object[] { null},
                new object[] { "2 notanint" },
                new object[] { "5 3 "},
                new object[] { "5   3 "},
                new object[] { "5 3 4"},
            };

            public IEnumerator<object[]> GetEnumerator() => unValidLawnLines.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class ValidMowerLine : IEnumerable<object[]>
        {
            private readonly List<object[]> validMowerLine = new List<object[]>()
            {
                new object[] { "5 5 N", 5 , 5 , 'N', Guid.NewGuid() },
                new object[] { "3 5 E", 3 , 5 , 'E', Guid.NewGuid() },
                new object[] { "0 5 W", 0 , 5 , 'W', Guid.NewGuid() },
                new object[] { "3 5 S", 3 , 5 , 'S', Guid.NewGuid() },
            };

            public IEnumerator<object[]> GetEnumerator() => validMowerLine.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class UnValidMowerLine : IEnumerable<object[]>
        {
            private readonly List<object[]> unValidMowerLine = new List<object[]>()
            {
                new object[] { null },
                new object[] { "" },
                new object[] { "1 2 Y" },
                new object[] { "5 3"},
                new object[] { "5   3S"},
                new object[] { "5 3 4"},
            };

            public IEnumerator<object[]> GetEnumerator() => unValidMowerLine.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
        public class ValidMowerCommandLine : IEnumerable<object[]>
        {
            private readonly List<object[]> validMowerCommandLine = new List<object[]>()
            {
                new object[] { "F", Guid.NewGuid(), new MowerCommandType[] { MowerCommandType.F } },
                new object[] { "R", Guid.NewGuid(), new MowerCommandType[] { MowerCommandType.R } },
                new object[] { "L", Guid.NewGuid(), new MowerCommandType[] { MowerCommandType.L } },
                new object[] { "FRL", Guid.NewGuid(), new MowerCommandType[] { MowerCommandType.F, MowerCommandType.R, MowerCommandType.L } }
            };

            public IEnumerator<object[]> GetEnumerator() => validMowerCommandLine.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        public class UnValidMowerCommandLine : IEnumerable<object[]>
        {
            private readonly List<object[]> unValidMowerCommandLine = new List<object[]>()
            {
                new object[] { "", Guid.NewGuid() },
                new object[] { null, Guid.NewGuid() },
                new object[] { "AFCZEC", Guid.NewGuid() },
                new object[] { "51848", Guid.NewGuid() },
                new object[] { "FRFRLRL ", Guid.NewGuid() },
                new object[] { "FRF RLRL", Guid.NewGuid() },
                new object[] { " FRFRLRL", Guid.NewGuid() }
            };

            public IEnumerator<object[]> GetEnumerator() => unValidMowerCommandLine.GetEnumerator();

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }
    }
}
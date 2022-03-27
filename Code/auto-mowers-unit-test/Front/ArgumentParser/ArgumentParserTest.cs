using auto_mowers.Front.ArgumentParser;
using auto_mowers.Front.ArgumentParser.Exception;
using System;
using System.Collections;
using System.Collections.Generic;
using Xunit;

namespace auto_mowers_unit_test.Front.ArgumentParserTest
{
    public class ArgumentParserTest
    {
        [Fact]
        public void Parse_SuccessFull_Returns_Value()
        {

            var argumentParser = new ArgumentParser(null);
            string[] args = { ArgumentParserTestData.validArg, ArgumentParserTestData.anyPath };
            var result = argumentParser.Parse(args);


            Assert.True(result != null, "return a result");
            Assert.Equal(result.FilePath, ArgumentParserTestData.anyPath);
        }

        [Fact]
        public void Parse_KO_CommandsKO_Returns_Value()
        {

            var argumentParser = new ArgumentParser(null);
            string[] args = { ArgumentParserTestData.validArg, ArgumentParserTestData.anyPath };
            var result = argumentParser.Parse(args);


            Assert.True(result != null, "return a result");
            Assert.Equal(result.FilePath, ArgumentParserTestData.anyPath);
        }

        [Theory()]
        [ClassData(typeof(ArgumentParserTestData.UnValidInputs))] // test paramétrés
        public void Parse_Wrong_Throws_Exception(string[] args)
        {
            var argumentParser = new ArgumentParser(null);

            Assert.Throws<ProgramArgumentException>(() => argumentParser.Parse(args));
        }
    }
}
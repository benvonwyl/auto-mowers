using auto_mowers.Front.ArgumentParser;
using auto_mowers.Front.ArgumentParser.Exception;
using auto_mowers.Front.FileParser;
using auto_mowers.Front.FileParser.Exception;
using auto_mowers.Services.MowerService.Contract;
using Moq;
using System;
using System.Collections.Generic;
using System.IO.Abstractions;
using Xunit;

namespace auto_mowers_unit_test.Front.FileParserTest
{
    public class FileParserTest
    {
        [Fact]
        public void LoadFileFromPath_SuccessFull_Returns_Value()
        {
            var path = FileParserTestData.ValidPath;
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fileS => fileS.File.ReadAllLines(path)).Returns(FileParserTestData.ValidData);

            var fileParser = new FileParser(null, mockFileSystem.Object);

            var result = fileParser.LoadFileFromPath(path);

            Assert.True(result != null, "return a result");
            Assert.Equal(result.Length, 5);
        }

        [Fact]
        public void LoadFileFromPath_KO_Returns_FileLoadException()
        {
            var path = FileParserTestData.ValidPath;
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fileS => fileS.File.ReadAllLines(path)).Throws<Exception>();

            var fileParser = new FileParser(null, mockFileSystem.Object);

            Assert.Throws<FileLoadException>(() => fileParser.LoadFileFromPath(path));
        }

        [Theory()]
        [ClassData(typeof(FileParserTestData.UnValidFileOutput))]
        public void LoadFileFromPath_KO_Returns_InvalidatefileOutputData_FileLoadException(string [] data)
        {
            var path = FileParserTestData.ValidPath;
            var mockFileSystem = new Mock<IFileSystem>();
            mockFileSystem.Setup(fileS => fileS.File.ReadAllLines(path)).Returns(data);

            var fileParser = new FileParser(null, mockFileSystem.Object);

            Assert.Throws<FileLoadException>(() => fileParser.LoadFileFromPath(path));
        }


        [Theory()]
        [ClassData(typeof(FileParserTestData.ValidLawnLine))]
        public void LoadLawnDefinitionLine_SuccessFull_Returns_Value(string line, int expectedX, int expectedY)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            var fileParser = new FileParser(null, mockFileSystem.Object);

            var result = fileParser.LoadLawnDefinitionLine(line);

            Assert.True(result != null, "return a result");
            Assert.Equal(result.X, expectedX);
            Assert.Equal(result.Y, expectedY);
        }

        [Theory()]
        [ClassData(typeof(FileParserTestData.UnValidLawnLine))]
        public void LoadLawnDefinitionLine_Invalid_Line_Returns_LineParsingException(string line)
            
        {
            var mockFileSystem = new Mock<IFileSystem>();
            var fileParser = new FileParser(null, mockFileSystem.Object);

            Assert.Throws<LineParsingException>(() => fileParser.LoadLawnDefinitionLine(line));
        }

        [Theory()]
        [ClassData(typeof(FileParserTestData.ValidMowerLine))]
        public void LoadMowerDefinitionLine_SuccessFull_Returns_Value(string line, int expectedX, int expectedY, char expectedO,Guid lawnGuid)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            var fileParser = new FileParser(null, mockFileSystem.Object);

            var result = fileParser.LoadMowerDefinitionLine(line,lawnGuid);

            Assert.True(result?.Position != null, "return a result");
            Assert.Equal(result.Position.X, expectedX);
            Assert.Equal(result.Position.Y, expectedY);
            Assert.Equal(result.Position.O, expectedO);
            Assert.Equal(result.LawnId, lawnGuid);
        }

        [Theory()]
        [ClassData(typeof(FileParserTestData.UnValidMowerLine))]
        public void LoadMowerDefinitionLine_Invalid_Line_Returns_LineParsingException(string line)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            var fileParser = new FileParser(null, mockFileSystem.Object);

            Assert.Throws<LineParsingException>(() => fileParser.LoadMowerDefinitionLine(line, Guid.NewGuid()));
        }

        [Theory()]
        [ClassData(typeof(FileParserTestData.ValidMowerCommandLine))]
        public void LoadMowerCommandsDefinitionLine_SuccessFull_Returns_Value(string line, Guid mowerId, MowerCommandType[] mowerCommandType)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            var fileParser = new FileParser(null, mockFileSystem.Object);

            var result = fileParser.LoadCommandsDefinitionLine(line, mowerId);

            Assert.True(result?.Commands != null, "return a result");
   

            for( int i = 0; i <result.Commands.Count; i++) {
                Assert.Equal(result.Commands[i], mowerCommandType[i]);
            }
            Assert.Equal(result.MowerId, mowerId);
        }

        [Theory()]
        [ClassData(typeof(FileParserTestData.UnValidMowerLine))]
        public void LoadMowerCommandsDefinitionLine_Invalid_Line_Returns_LineParsingException(string line)
        {
            var mockFileSystem = new Mock<IFileSystem>();
            var fileParser = new FileParser(null, mockFileSystem.Object);

            Assert.Throws<LineParsingException>(() => fileParser.LoadCommandsDefinitionLine(line, Guid.NewGuid()));
        }

    }
}
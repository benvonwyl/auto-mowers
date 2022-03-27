using auto_mowers.Front.ArgumentParser.Contract;
using auto_mowers.Front.ArgumentParser.Exception;
using auto_mowers.Front.AutoMowersFront;
using auto_mowers.Front.AutoMowersFront.Contract;
using auto_mowers.Front.AutoMowersFront.Exception;
using auto_mowers.Front.FileParser.Contract;
using auto_mowers.Front.FileParser.Exception;
using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.LawnService.Exception;
using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerCommandService.Contract;
using auto_mowers.Services.MowerCommandService.Exception;
using auto_mowers.Services.MowerFront.Contract;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Exception;
using auto_mowers.Services.MowerService.Request;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;
using static auto_mowers_unit_test.Front.MowersFrontTestData.MowersFrontTestData;

namespace auto_mowers_unit_test.Front.MowersFrontTest
{
    public class MowersFrontTest
    {
        [Fact]
        public void LoadFromFileDefinitionLine_SuccessFull_Returns_Value()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var lawnServiceMock = new Mock<ILawnService>();
            var fileParserMock = new Mock<IFileParser>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            string filepath = "validapath";

            string[] lines = new string[] { "5 5", "1 2 N", "LRF" };

            AddLawnRequest lawnRequest = new AddLawnRequest { };
            Guid lawnGuid = Guid.NewGuid();
            AddMowerRequest mowerRequest = new AddMowerRequest { };
            Guid mowerGuid = Guid.NewGuid();

            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Returns(lawnGuid);
            fileParserMock.Setup(t => t.LoadMowerDefinitionLine(lines[1], lawnGuid)).Returns(mowerRequest);
            mowerServiceMock.Setup(t => t.AddMower(mowerRequest)).Returns(mowerGuid);

            var mowercommands = new MowerCommands { };

            fileParserMock.Setup(t => t.LoadCommandsDefinitionLine(lines[2], mowerGuid)).Returns(mowercommands);

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);
            var rep = mowerServiceFront.LoadFromFile(filepath);

            Assert.Equal(mowercommands, rep[0]);

            fileParserMock.Verify(t => t.LoadFileFromPath(filepath), Times.Exactly(1));

            fileParserMock.Verify(t => t.LoadLawnDefinitionLine(lines[0]), Times.Exactly(1));
            fileParserMock.Verify(t => t.LoadMowerDefinitionLine(lines[1], lawnGuid), Times.Exactly(1));
            fileParserMock.Verify(t => t.LoadCommandsDefinitionLine(lines[2], mowerGuid), Times.Exactly(1));

            lawnServiceMock.Verify(t => t.AddLawn(lawnRequest), Times.Exactly(1));
            mowerServiceMock.Verify(t => t.AddMower(mowerRequest), Times.Exactly(1));
        }

        [Fact]
        public void LoadFromFile_LineParsingException_Command_Returns_Exception()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            string filepath = "validapath";
            string[] lines = new string[] { "5 5", "1 2 N", "LFLFLFRLFF" };
            AddLawnRequest lawnRequest = new AddLawnRequest { };
            Guid lawnGuid = Guid.NewGuid();
            AddMowerRequest mowerRequest = new AddMowerRequest { };
            Guid mowerGuid = Guid.NewGuid();

            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Returns(lawnGuid);
            fileParserMock.Setup(t => t.LoadMowerDefinitionLine(lines[1], lawnGuid)).Returns(mowerRequest);
            mowerServiceMock.Setup(t => t.AddMower(mowerRequest)).Returns(mowerGuid);
            fileParserMock.Setup(t => t.LoadCommandsDefinitionLine(lines[2], mowerGuid)).Throws(new LineParsingException("blll", new Exception("initial EX")));

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);
            Assert.Throws<MowerDataFromFileLoadException>(() => mowerServiceFront.LoadFromFile(filepath));
        }
        [Fact]
        public void LoadFromFile_InvalidMowerException_Returns_Exception()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            string filepath = "validapath";
            string[] lines = new string[] { "5 5", "1 2 N", "LFLFLFRLFF" };
            AddLawnRequest lawnRequest = new AddLawnRequest { };
            Guid lawnGuid = Guid.NewGuid();
            AddMowerRequest mowerRequest = new AddMowerRequest { };


            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Returns(lawnGuid);
            fileParserMock.Setup(t => t.LoadMowerDefinitionLine(lines[1], lawnGuid)).Returns(mowerRequest);
            mowerServiceMock.Setup(t => t.AddMower(mowerRequest)).Throws(new InvalidMowerException("blll"));

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);
            Assert.Throws<MowerDataFromFileLoadException>(() => mowerServiceFront.LoadFromFile(filepath));
        }

        [Fact]
        public void LoadFromFile_LineParsingException_Mower_Returns_Exception()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            string filepath = "validapath";
            string[] lines = new string[] { "5 5", "1 2 N", "LFLFLFRLFF" };
            AddLawnRequest lawnRequest = new AddLawnRequest { };
            Guid lawnGuid = Guid.NewGuid();


            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Returns(lawnGuid);
            fileParserMock.Setup(t => t.LoadMowerDefinitionLine(lines[1], lawnGuid)).Throws(new LineParsingException("blll"));

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);
            Assert.Throws<MowerDataFromFileLoadException>(() => mowerServiceFront.LoadFromFile(filepath));
        }

        [Fact]
        public void LoadFromFile_InvalidLawnException_Returns_Exception()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            string filepath = "validapath";
            string[] lines = new string[] { "5 5", "1 2 N", "LFLFLFRLFF" };
            AddLawnRequest lawnRequest = new AddLawnRequest { };

            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Throws(new InvalidLawnException("blbllbl"));

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);
            Assert.Throws<MowerDataFromFileLoadException>(() => mowerServiceFront.LoadFromFile(filepath));
        }
        [Fact]
        public void LoadFromFile_LineParsingException_Lawn_Returns_Exception()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            string filepath = "validapath";
            string[] lines = new string[] { "5 5", "1 2 N", "LFLFLFRLFF" };

            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Throws(new LineParsingException("blll"));

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);
            Assert.Throws<MowerDataFromFileLoadException>(() => mowerServiceFront.LoadFromFile(filepath));
        }
        [Fact]
        public void LoadFromFile_FileLoadException_Lawn_Returns_Exception()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            string filepath = "validapath";

            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Throws(new FileLoadException("llllll"));

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);
            Assert.Throws<MowerDataFromFileLoadException>(() => mowerServiceFront.LoadFromFile(filepath));
        }

        [Fact]
        public void RunAutoMowers_InRightOrder_SuccessFull_Returns_Value()
        {
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            var commands1 = new MowerCommands() { Commands = new List<MowerCommandType>(), MowerId = Guid.NewGuid() };
            commands1.Commands.Add(MowerCommandType.F);

            var commands2 = new MowerCommands() { Commands = new List<MowerCommandType>(), MowerId = Guid.NewGuid() };
            commands2.Commands.Add(MowerCommandType.R);

            Position position1 = new Position() { X = 1, Y = 1, O = OrientationType.N };
            Position position2 = new Position() { X = 1, Y = 1, O = OrientationType.N };

            List<MowerCommands> commands = new List<MowerCommands>();

            commands.Add(commands1);
            commands.Add(commands2);

            var args = new List<Guid>();
            var args2 = new List<MowerCommandType>();

            _mowerCommandSeviceMock.Setup(t => t.ExecuteMowerCommand(Capture.In(args), Capture.In(args2))).Returns(position1);

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);

            mowerServiceFront.RunAutoMowers(commands);

            _mowerCommandSeviceMock.Verify(t => t.ExecuteMowerCommand(commands1.MowerId, commands1.Commands[0]), Times.Exactly(1));
            _mowerCommandSeviceMock.Verify(t => t.ExecuteMowerCommand(commands2.MowerId, commands2.Commands[0]), Times.Exactly(1));
            Assert.Equal(args.Count, 2);
            Assert.Equal(args2.Count, 2);
            Assert.Equal(args[0], commands1.MowerId);
            Assert.Equal(args2[0], commands1.Commands[0]);
            Assert.Equal(args[1], commands2.MowerId);
            Assert.Equal(args2[1], commands2.Commands[0]);
        }

        [Theory()]
        [ClassData(typeof(UnvalidRunMowersinput))]
        public void RunAutoMowers_InvalidArgs_Returns_Exception(List<MowerCommands> mc)
        {
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var _argumentParserMock = new Mock<IArgumentParser>();
            var lawnServiceMock = new Mock<ILawnService>();

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);

            Assert.Throws<MowerExecutionException>(() => mowerServiceFront.RunAutoMowers(mc));
        }

        [Fact]
        public void RunAutoMowers_ExecuteMowerCommandException_Returns_Exception()
        {
            var guid = Guid.NewGuid();
            var mct = MowerCommandType.L;
            var input = new List<MowerCommands>();
            var item = new MowerCommands() { };
            item.Commands.Add(mct);
            item.MowerId = guid;
            input.Add(item);

            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();

            _mowerCommandSeviceMock.Setup(t => t.ExecuteMowerCommand(guid, mct)).Throws(new ExecuteMowerCommandException("exception", new Exception("initial ex")));

            var mowerServiceFront = new MowersFront(null, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);

            Assert.Throws<MowerExecutionException>(() => mowerServiceFront.RunAutoMowers(input));
        }

        [Fact]
        public void Run_OK_Returns_0()
        {

            string filepath = "validapath";
            var args = new string[] { "--filepath", filepath };
            var argument = new Arguments { FilePath = filepath };

            string[] lines = new string[] { "5 5", "1 2 N", "L" };

            AddLawnRequest lawnRequest = new AddLawnRequest { };
            Guid lawnGuid = Guid.NewGuid();
            AddMowerRequest mowerRequest = new AddMowerRequest { };
            Guid mowerGuid = Guid.NewGuid();

            var mct = MowerCommandType.L;
            var command = new MowerCommands() { MowerId = mowerGuid };
            command.Commands.Add(MowerCommandType.L);
            var res = new Position() { X = 1, Y = 2, O = OrientationType.W };

            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();
            var _logger = new Mock<ILogger<MowersFront>>();

            _argumentParserMock.Setup(t => t.Parse(args)).Returns(argument);
            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Returns(lawnGuid);
            fileParserMock.Setup(t => t.LoadMowerDefinitionLine(lines[1], lawnGuid)).Returns(mowerRequest);
            mowerServiceMock.Setup(t => t.AddMower(mowerRequest)).Returns(mowerGuid);
            fileParserMock.Setup(t => t.LoadCommandsDefinitionLine(lines[2], mowerGuid)).Returns(command);

            _mowerCommandSeviceMock.Setup(t => t.ExecuteMowerCommand(mowerGuid, mct)).Returns(res);

            var mowerServiceFront = new MowersFront(_logger.Object, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);

            int result = mowerServiceFront.Run(args);

            Assert.Equal(result, 0);
        }

        [Fact]
        public void Run_ExecuteMowerCommandKO_Returns_2()
        {

            string filepath = "validapath";
            var args = new string[] { "--filepath", filepath };
            var argument = new Arguments { FilePath = filepath };

            string[] lines = new string[] { "5 5", "1 2 N", "L" };

            AddLawnRequest lawnRequest = new AddLawnRequest { };
            Guid lawnGuid = Guid.NewGuid();
            AddMowerRequest mowerRequest = new AddMowerRequest { };
            Guid mowerGuid = Guid.NewGuid();

            var mct = MowerCommandType.L;
            var command = new MowerCommands() { MowerId = mowerGuid };
            command.Commands.Add(MowerCommandType.L);
            var res = new Position() { X = 1, Y = 2, O = OrientationType.W };

            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();
            var _logger = new Mock<ILogger<MowersFront>>();

            _argumentParserMock.Setup(t => t.Parse(args)).Returns(argument);
            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Returns(lawnGuid);
            fileParserMock.Setup(t => t.LoadMowerDefinitionLine(lines[1], lawnGuid)).Returns(mowerRequest);
            mowerServiceMock.Setup(t => t.AddMower(mowerRequest)).Returns(mowerGuid);
            fileParserMock.Setup(t => t.LoadCommandsDefinitionLine(lines[2], mowerGuid)).Returns(command);

            _mowerCommandSeviceMock.Setup(t => t.ExecuteMowerCommand(mowerGuid, mct)).Throws(new Exception());

            var mowerServiceFront = new MowersFront(_logger.Object, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);

            int result = mowerServiceFront.Run(args);

            _logger.Verify(t => t.Log(LogLevel.Error,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);

            Assert.Equal(result, 2);
        }

        [Fact]
        public void Run_ParseLine_Returns_2()
        {

            string filepath = "validapath";
            var args = new string[] { "--filepath", filepath };
            var argument = new Arguments { FilePath = filepath };

            string[] lines = new string[] { "5 5", "1 2 N", "L" };

            AddLawnRequest lawnRequest = new AddLawnRequest { };
            Guid lawnGuid = Guid.NewGuid();

            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();
            var _logger = new Mock<ILogger<MowersFront>>();

            _argumentParserMock.Setup(t => t.Parse(args)).Returns(argument);
            fileParserMock.Setup(t => t.LoadFileFromPath(filepath)).Returns(lines);
            fileParserMock.Setup(t => t.LoadLawnDefinitionLine(lines[0])).Returns(lawnRequest);
            lawnServiceMock.Setup(t => t.AddLawn(lawnRequest)).Throws(new Exception());

            var mowerServiceFront = new MowersFront(_logger.Object, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);

            int result = mowerServiceFront.Run(args);

            _logger.Verify(t => t.Log(LogLevel.Error,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);

            Assert.Equal(result, 2);
        }
        [Fact]
        public void Run_ArgParser_KO_Returns_2()
        {
            string filepath = "validapath";
            var args = new string[] { "--filepath", filepath };
           
            var mowerServiceMock = new Mock<IMowerService>();
            var fileParserMock = new Mock<IFileParser>();
            var lawnServiceMock = new Mock<ILawnService>();
            var _mowerCommandSeviceMock = new Mock<IMowerCommandService>();
            var _argumentParserMock = new Mock<IArgumentParser>();
            var _logger = new Mock<ILogger<MowersFront>>();

            _argumentParserMock.Setup(t => t.Parse(args)).Throws(new ProgramArgumentException("except"));

            var mowerServiceFront = new MowersFront(_logger.Object, mowerServiceMock.Object, fileParserMock.Object, lawnServiceMock.Object, _mowerCommandSeviceMock.Object, _argumentParserMock.Object);

            int result = mowerServiceFront.Run(args);

            _logger.Verify(t => t.Log(LogLevel.Error,
               It.IsAny<EventId>(),
               It.IsAny<It.IsAnyType>(),
               It.IsAny<Exception>(),
               (Func<It.IsAnyType, Exception, string>)It.IsAny<object>()), Times.Once);

            Assert.Equal(result, 2);
        }
    }
}
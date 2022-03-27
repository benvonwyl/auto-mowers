using auto_mowers.Front.ArgumentParser;
using auto_mowers.Front.ArgumentParser.Exception;
using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.LawnService.Exception;
using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerCommandService;
using auto_mowers.Services.MowerCommandService.Exception;
using auto_mowers.Services.MowerService;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Exception;
using auto_mowers.Services.MowerService.Request;
using Moq;
using System;
using Xunit;

namespace auto_mowers_unit_test.Services.MowerCommandServiceTest
{
    public class MowerCommandServiceTest
    {
        [Theory()]
        [ClassData(typeof(MowerCommandServiceTestData.ValidMowerCommand))]
        public void ExecuteMowerCommand_SuccessFull_Returns_Value(Lawn lawn, Guid lawnId , Mower mower, Guid mowerId, MowerCommandType m,Position p, string name)
        {
            var lawnService = new Mock<ILawnService>();
            lawnService.Setup(t => t.GetLawnById(lawnId)).Returns(lawn);

            var mowerService = new Mock<IMowerService>();
            mowerService.Setup(t => t.GetMower(mowerId)).Returns(mower);

            var mowerCommandService = new MowerCommandService(null,lawnService.Object, mowerService.Object);
            var result = mowerCommandService.ExecuteMowerCommand(mowerId, m);
            
            Assert.NotNull(result);
            Assert.True(result.X == p.X, name + " check final x");
            Assert.True(result.Y == p.Y, name + " check final y");
            Assert.True(result.O == p.O, name + " check final o");
        }

        [Fact]
        public void ExecuteMowerCommand_MowerServiceException_Returns_Exception()
        {
            var lawnService = new Mock<ILawnService>();
            lawnService.Setup(t => t.GetLawnById(It.IsAny<Guid>())).Returns(new Lawn());

            var mowerService = new Mock<IMowerService>();
            mowerService.Setup(t => t.GetMower(It.IsAny<Guid>())).Throws(new MowerNotFoundException("exception"));

            var mowerCommandService = new MowerCommandService(null, lawnService.Object, mowerService.Object);
            
            Assert.Throws<ExecuteMowerCommandException>(() => mowerCommandService.ExecuteMowerCommand(It.IsAny<Guid>(), It.IsAny<MowerCommandType>()));
        }

        [Fact]
        public void ExecuteMowerCommand_LawnServiceException_Returns_Exception()
        {
            var lawnService = new Mock<ILawnService>();
            lawnService.Setup(t => t.GetLawnById(It.IsAny<Guid>())).Throws<LawnNotfoundException>();

            var mowerService = new Mock<IMowerService>();

            var mowerCommandService = new MowerCommandService(null, lawnService.Object, mowerService.Object);

            Assert.Throws<ExecuteMowerCommandException>(() => mowerCommandService.ExecuteMowerCommand(It.IsAny<Guid>(), It.IsAny<MowerCommandType>()));
        }
    }
}
using auto_mowers.Front.ArgumentParser;
using auto_mowers.Front.ArgumentParser.Exception;
using auto_mowers.Services.LawnService.Contract;
using auto_mowers.Services.LawnService.Exception;
using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerService;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Exception;
using auto_mowers.Services.MowerService.Request;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace auto_mowers_unit_test.Services.MowerServiceTest
{
    public class MowerServiceTest
    {
        [Theory()]
        [ClassData(typeof(MowerServiceTestData.ValidMowerAddRequest))]
        public void AddMower_SuccessFull_Returns_Value(Lawn lawn, Guid lawnId, AddMowerRequest request, int x, int y, char o, string name)
        {
            var lawnService = new Mock<ILawnService>();

            lawnService.Setup(t => t.GetLawnById(lawnId)).Returns(lawn);

            var mowerService = new MowerService(null, lawnService.Object);

            var mowerId = mowerService.AddMower(request);
            var mower = mowerService.GetMower(mowerId);

            Assert.True(mower.P.X == x);
            Assert.True(mower.P.Y == y);
            Assert.True(mower.P.O.ToString() == o.ToString());
            Assert.True(mower.LawnId == lawnId);
        }

        [Theory()]
        [ClassData(typeof(MowerServiceTestData.InValidMowerAddRequest))]
        public void AddMower_InvalidRequest_Returns_Exception(Lawn lawn, Guid lawnId, AddMowerRequest request)
        {
            var lawnService = new Mock<ILawnService>();
            lawnService.Setup(t => t.GetLawnById(lawnId)).Returns(lawn);

            lawnService.Setup(t => t.GetLawnById(It.Is<Guid>(guid => guid != lawnId))).Throws(new LawnNotfoundException(".."));

            var mowerService = new MowerService(null, lawnService.Object);

            Assert.Throws<InvalidMowerException>(() => mowerService.AddMower(request));
        }

        [Fact]
        public void GetMower_SuccessFull_Returns_Value()
        {
            Guid lawnId = Guid.NewGuid();
            Lawn lawn = new Lawn() { X = 3, Y = 3 };

            var lawnService = new Mock<ILawnService>();
            lawnService.Setup(t => t.GetLawnById(lawnId)).Returns(lawn);

            var mowerService = new MowerService(null, lawnService.Object);
            var mowerPosition = new PositionRequest() { X = 2, Y = 1, O = 'N' };

            Guid id = mowerService.AddMower(new AddMowerRequest { LawnId = lawnId, Position = mowerPosition });

            var result = mowerService.GetMower(id);

            Assert.NotNull(result);
            Assert.True(result.LawnId == lawnId);
            Assert.Equal(result.P?.X, mowerPosition.X);
            Assert.Equal(result.P?.Y, mowerPosition.Y);
            Assert.Equal(result.P?.O.ToString(), mowerPosition.O.ToString());
        }

        [Fact]
        public void GetMower_KO_Throws_MowerNotFoundException()
        {
            var lawnService = new Mock<ILawnService>();
            var mowerService = new MowerService(null, lawnService.Object);

            Guid id = Guid.NewGuid();
            Assert.Throws<MowerNotFoundException>(() => mowerService.GetMower(id));
        }
    }
}
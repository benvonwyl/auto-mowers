using auto_mowers.Front.ArgumentParser;
using auto_mowers.Front.ArgumentParser.Exception;
using auto_mowers.Services.LawnService.Exception;
using auto_mowers.Services.LawnService.Request;
using auto_mowers.Services.MowerService;
using auto_mowers.Services.MowerService.Contract;
using auto_mowers.Services.MowerService.Exception;
using auto_mowers.Services.MowerService.Request;
using System;
using Xunit;

namespace auto_mowers_unit_test.Services.LawnServiceTest
{
    public class LawnServiceTest
    {
        [Theory()]
        [ClassData(typeof(LawnServiceTestData.ValidLawnAddRequest))]
        public void AddLawn_SuccessFull_Returns_Value(AddLawnRequest request, int x, int y, string name)
        {
            var lawnService = new LawnService(null);

            var result = lawnService.AddLawn(request);

            Assert.True(lawnService.Lawns.ContainsKey(result), name);
            Assert.True(lawnService.Lawns[result].X == x && lawnService.Lawns[result].Y == y, name);
        }

        [Theory()]
        [ClassData(typeof(LawnServiceTestData.InValidLawnAddRequest))]
        public void AddLawn_InvalidRequest_Returns_Exception(AddLawnRequest request)
        {
            var lawnService = new LawnService(null);

            Assert.Throws<InvalidLawnException>(() => lawnService.AddLawn(request));
            Assert.True(lawnService.Lawns.Count == 0);
        }

        [Fact]
        public void GetLawnById_SuccessFull_Returns_Value()
        {

            var lawnService = new LawnService(null);

            Guid id = lawnService.AddLawn(new AddLawnRequest() { X = 1, Y = 1 });

            var result = lawnService.GetLawnById(id);

            Assert.NotNull(result);
            Assert.True(result.X == 1);
            Assert.True(result.Y == 1);
        }

        [Fact]
        public void GetLawnById_KO_Returns_Value()
        {
            var lawnService = new LawnService(null);
            Guid id = Guid.NewGuid();
            Assert.Throws<LawnNotfoundException>(() => lawnService.GetLawnById(id));
        }
    }
}
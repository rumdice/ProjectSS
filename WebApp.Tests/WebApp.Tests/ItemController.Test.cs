using Moq;
using Xunit;
using System.Collections.Generic;
using System.Linq;
using WebApp.Service;
using WebApp.Controller;
using CoreLibrary.Database;
using WebApp.ViewModels;

namespace WebApiProject.Tests
{
    public class ItemControllerTests
    {
        [Fact]
        public void Get_ReturnsWeatherForecasts()
        {
            // Arrange
            var mockService = new Mock<ItemService>();
            mockService.Setup(service => service.GetItemSimpleInfoListByNameAsync("")).Returns(GetTestForecastsAsync()); // ?
            var controller = new ItemController(null, mockService.Object);

            // Act
            
            // req 패킷 제작
            var request = new GetItemSimpleInfoViewModelRequest
            {
                UserUid = 1,
                ItemTid = 1,
                ItemUid = 1
            };

            // POST
            var result = controller.GetItemSimpleInfoAsync(request); // 컨트롤러의 POST 호출
            
            // GET
            // TODO : 새로 만든 Get 메서드는 왜 안잡히지?
            // long itemTid = 1001;
            // var result2 = controller.GetAsync(itemTid);

            // Assert
            var forecasts = Assert.IsAssignableFrom<IEnumerable<ItemSimpleEntity>>(result); // 의도한 결과값과 실제로 리턴되어 온 값이 일치하는지 확인한다.
            Assert.Equal(5, forecasts.Count());
        }

        private Task<List<ItemSimpleEntity>> GetTestForecastsAsync()
        {
            // TODO: 의도한 결과 값을 하드코딩해서 넣는다.
            return null;
        }
    }
}
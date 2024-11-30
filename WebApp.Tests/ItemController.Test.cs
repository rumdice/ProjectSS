using Moq;
using WebApp.Service;
using WebApp.Controller;
using CoreDB.Database;
using WebApp.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using WepApp.DtoModels;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Mvc;
using WebApp.Models;
using CoreLibrary.ViewModels;

namespace WebApiProject.Tests
{
    public class ItemControllerTests
    {
        private readonly Mock<ItemService> _mockItemService;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<ILogger<ItemController>> _mockLogger;

        public ItemControllerTests()
        {
            _mockItemService = new Mock<ItemService>(
                Mock.Of<IServiceProvider>(),
                Mock.Of<IHttpContextAccessor>(),
                Mock.Of<ILogger<ItemService>>());
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockLogger = new Mock<ILogger<ItemController>>();
        }

        [Fact]
        public async Task GetItemSimpleInfo_ReturnsExpectedResultAsync()
        {
            // Arrange
            var expectedItemTid = 1001;
            var expectedItemDto = new ItemSimpleInfoDto
            {
                ItemTid = expectedItemTid,
                Name = "테스트 아이템",
                Grade = 1
            };

            var request = new GetItemSimpleInfoViewModelRequest
            {
                ItemTid = (ulong)expectedItemTid
            };

            _mockItemService.Setup(service => service.GetSimpleItemResultAsync(It.IsAny<long>()))
            //_mockItemService.Setup(service => service.GetSimpleItemResultAsync(expectedItemTid))
                .ReturnsAsync(new ItemSimpleEntity
                {
                    ItemTid = expectedItemTid,
                    Name = "테스트 아이템",
                    Grade = 1
                });

            var serviceProvider = new ServiceCollection()
                .AddSingleton(_mockItemService.Object)
                .BuildServiceProvider();

            
            var controller = new ItemController(
                serviceProvider,
                _mockHttpContextAccessor.Object,
                _mockLogger.Object);

            // Act
            var result = await controller.GetItemSimpleInfo(request);

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var response = Assert.IsType<GetItemSimpleInfoViewModelResponse>(okResult.Value);
            Assert.Equal(ServiceResponseCode.Success, response.ResponesCode);
            Assert.Equal(expectedItemDto.ItemTid, response.ItemSimpleInfo.ItemTid);
            Assert.Equal(expectedItemDto.Name, response.ItemSimpleInfo.Name);
            Assert.Equal(expectedItemDto.Grade, response.ItemSimpleInfo.Grade);
            
        }
    }
}
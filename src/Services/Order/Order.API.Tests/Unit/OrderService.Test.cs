using Domain.Common.Exceptions;
using Domain.Common.Generators;
using Domain.Common.Services;
using Microsoft.Extensions.Configuration;
using Moq;
using Order.API.Services;

namespace Order.API.Tests.Unit;

[TestClass]
public class OrderServiceTests
{
    public static IConfiguration InitConfiguration()
    {
        var config = new ConfigurationBuilder()
           .AddJsonFile("appsettings.test.json")
            .AddEnvironmentVariables()
            .Build();
        return config;
    }

    [TestMethod]
    public void GetMenuItems_Returns_Default_List()
    {
        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);
        var config = InitConfiguration();

        mock.Setup(service => service.PublishEvent(config["RMQ_HOST"], It.IsAny<string>(), config["DL_INTERNAL_TAG"])).Verifiable();
        var mockService = mock.Object;

        var service = new OrderService(mockService, config);

        var result = service.GetMenuItems();

        Assert.IsNotNull(result);
        Assert.AreNotEqual(0, result.Count);
        foreach (var item in result)
        {
            Assert.IsFalse(string.IsNullOrEmpty(item.Name));
            Assert.IsNotNull(item.PreparationTime);
            Assert.IsNotNull(item.Price);
        }
    }

    [TestMethod]
    public void AddOrder_Maps_Items_Correctly()
    {
        // arrange
        var config = InitConfiguration();
        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);

        mock.Setup(service => service.PublishEvent(config["RMQ_HOST"], It.IsAny<string>(), $"dl-exchange-{config["DL_INTERNAL_TAG"]}")).Verifiable();
        var mockService = mock.Object;
        var postData = TestDataGenerator.GetTestOrderCreate();
        var service = new OrderService(mockService, config);

        // act
        var result = service.AddOrder(postData);
        mock.Verify(mock => mock.PublishEvent(config["RMQ_HOST"], It.IsAny<string>(), $"dl-exchange-{config["DL_INTERNAL_TAG"]}"), Times.AtLeast(1));

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(postData.MenuItems.Count, result.Items.Count);
        Assert.IsInstanceOfType(result.Id, typeof(Guid));
    }
}

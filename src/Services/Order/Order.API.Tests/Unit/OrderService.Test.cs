using Domain.Common.Exceptions;
using Domain.Common.Generators;
using Domain.Common.Services;
using Moq;
using Order.API.Services;

namespace Order.API.Tests.Unit;

[TestClass]
public class OrderServiceTests
{
    private readonly string _tag = "Vtest";
    private readonly string _rmqHost = "testhost";

    [TestInitialize]
    public void TestInit()
    {
        Environment.SetEnvironmentVariable("DL_TAG_VERSION", "Vtest");
        Environment.SetEnvironmentVariable("RMQ_HOST", "testhost");
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "test");
    }

    [TestMethod]
    public void GetMenuItems_Returns_Default_List()
    {
        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);

        mock.Setup(service => service.PublishEvent(_rmqHost, It.IsAny<string>(), $"exchange-{_tag}")).Verifiable();
        var mockService = mock.Object;

        var service = new OrderService(mockService);

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
        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);

        mock.Setup(service => service.PublishEvent(_rmqHost, It.IsAny<string>(), $"exchange-{_tag}")).Verifiable();
        var mockService = mock.Object;
        var postData = TestDataGenerator.GetTestOrderCreate();
        var service = new OrderService(mockService);

        // act
        var result = service.AddOrder(postData);
        mock.Verify(mock => mock.PublishEvent(_rmqHost, It.IsAny<string>(), $"exchange-{_tag}"), Times.AtLeast(1));

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(postData.MenuItems.Count, result.Items.Count);
        Assert.IsInstanceOfType(result.Id, typeof(Guid));
    }

    [TestMethod]
    public void AddOrder_ThrowsException_IfNoDLTagVersionInEnvironment()
    {
        Environment.SetEnvironmentVariable("DL_TAG_VERSION", "");
        // arrange
        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);

        mock.Setup(service => service.PublishEvent(_rmqHost, It.IsAny<string>(), $"exchange-{_tag}")).Verifiable();
        var mockService = mock.Object;
        var postData = TestDataGenerator.GetTestOrderCreate();
        var service = new OrderService(mockService);

        // act "The environment variable 'DL_TAG_VERSION' was not set."
        Assert.ThrowsException<EnvironmentVariableException>(() => service.AddOrder(postData));
        var correctMessage = Assert.ThrowsException<EnvironmentVariableException>(() => service.AddOrder(postData))
            .Message.Equals("The environment variable 'DL_TAG_VERSION' was not set.");

        Assert.IsTrue(correctMessage);
    }

    [TestMethod]
    public void AddOrder_ThrowsException_IfNoRMQHostInEnvironment()
    {
        Environment.SetEnvironmentVariable("RMQ_HOST", "");

        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);

        mock.Setup(service => service.PublishEvent(_rmqHost, It.IsAny<string>(), $"exchange-{_tag}")).Verifiable();
        var mockService = mock.Object;
        var postData = TestDataGenerator.GetTestOrderCreate();
        var service = new OrderService(mockService);

        // act
        Assert.ThrowsException<EnvironmentVariableException>(() => service.AddOrder(postData));
        var correctMessage = Assert.ThrowsException<EnvironmentVariableException>(() => service.AddOrder(postData))
            .Message.Equals("The environment variable 'RMQ_HOST' was not set.");

        Assert.IsTrue(correctMessage);
    }

    [TestMethod]
    public void AddOrder_ThrowsException_IfNoDotnetEnvInEnvironment()
    {
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "");

        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);

        mock.Setup(service => service.PublishEvent(_rmqHost, It.IsAny<string>(), $"exchange-{_tag}")).Verifiable();
        var mockService = mock.Object;
        var postData = TestDataGenerator.GetTestOrderCreate();
        var service = new OrderService(mockService);

        // act
        Assert.ThrowsException<EnvironmentVariableException>(() => service.AddOrder(postData));
        var correctMessage = Assert.ThrowsException<EnvironmentVariableException>(() => service.AddOrder(postData))
            .Message.Equals("The environment variable 'DOTNET_ENVIRONMENT' was not set.");

        Assert.IsTrue(correctMessage);
    }
}

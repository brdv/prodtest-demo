using Domain.Common.Services;
using Moq;
using Order.API.Services;
using Order.API.Tests.TestData;

namespace Order.API.Tests.Unit;

[TestClass]
public class OrderServiceTests
{
    [AssemblyInitialize]
    public static void AssemblyInit(TestContext context)
    {
        Environment.SetEnvironmentVariable("DL_TAG_VERSION", "Vtest");
        Environment.SetEnvironmentVariable("RMQ_HOST", "testhost");
        Environment.SetEnvironmentVariable("DOTNET_ENVIRONMENT", "test");

    }

    [TestMethod]
    public void GetMenuItems_Returns_Default_List()
    {
        var mock = new Mock<IRabbitMQService>(MockBehavior.Strict);

        mock.Setup(service => service.PublishEvent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
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

        mock.Setup(service => service.PublishEvent(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())).Verifiable();
        var mockService = mock.Object;
        var postData = OrderTestData.GetTestOrderCreate();
        var service = new OrderService(mockService);

        // act
        var result = service.AddOrder(postData);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(postData.MenuItems.Count, result.Items.Count);
        Assert.IsInstanceOfType(result.Id, typeof(Guid));
    }
}

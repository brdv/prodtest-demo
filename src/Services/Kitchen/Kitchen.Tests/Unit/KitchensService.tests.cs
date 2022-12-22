using Domain.Common.Generators;
using Domain.Common.Models;
using Kitchen.DAL;
using Kitchen.Services;
using Moq;

namespace Kitchen.Tests.Unit;

[TestClass]
public class KitchenServiceTests
{
    [TestMethod]
    public void HandleOrder_CallsAddHandledOrder_OnMockRepo()
    {
        // Arrange
        var mock = new Mock<IKitchenRepository>();
        mock.Setup(repo => repo.AddHandledOrder(It.IsAny<HandledOrder>())).Verifiable();
        var mockRepo = mock.Object;
        var service = new KitchenService(mockRepo);
        var order = TestDataGenerator.GetTestOrderModel();

        // Act
        service.HandleOrder(order, "tag");

        // Assert
        mock.Verify(repo => repo.AddHandledOrder(It.IsAny<HandledOrder>()));
    }
}

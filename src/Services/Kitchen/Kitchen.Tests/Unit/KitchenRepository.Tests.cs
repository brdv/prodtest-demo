using Domain.Common.Generators;
using Domain.Common.Models;
using Kitchen.DAL;
using Kitchen.Services;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace Kitchen.Tests.Unit;

[TestClass]
public class KitchenRepositoryTests
{
    [TestMethod]
    public async Task AddHandledOrder_CallsAddAsync_OnContext()
    {
        // Arrange
        var testHandledOrder = TestDataGenerator.GetTestHandledOrder();

        var mock = new Mock<KitchenDbContext>();
        mock.Setup(c => c.AddAsync(testHandledOrder, It.IsAny<CancellationToken>())).Verifiable();

        var repo = new KitchenRepository(mock.Object);

        // Act
        await repo.AddHandledOrder(testHandledOrder);

        // Assert
        mock.Verify(c => c.AddAsync(testHandledOrder, It.IsAny<CancellationToken>()), Times.Once());
    }
}

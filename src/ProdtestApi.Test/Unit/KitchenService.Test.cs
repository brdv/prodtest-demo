
using ProdtestApi.Models;
using ProdtestApi.Services;

namespace ProdtestApi.Test.Unit;

[TestClass]
public class KitchenServiceTest
{
    Order order = new Order(
        Guid.NewGuid(),
        new List<MenuItem>(){
            new MenuItem(Guid.NewGuid(), "Burger", 4, 2.50),
            new MenuItem(Guid.NewGuid(), "Soda", 1, 1.50),
            new MenuItem(Guid.NewGuid(), "Fries", 2, 1.50)
        }
  );

    [TestMethod]
    public void HandleOrder_WithValidOrder_ReturnsOrderHandeled()
    {
        IKitchenService service = new KitchenService();

        var result = service.handleOrder(order);

        Assert.AreEqual(result.orderId, order.Id);
        Assert.AreEqual(result.totalPrepTime, order.TotalPrepTime);
        Assert.IsNotNull(result.actualPrepTime);

    }
}
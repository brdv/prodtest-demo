using Order.API.Models;
namespace Order.API.Test.Unit;


[TestClass]
public class OrderModelTests
{
    List<MenuItem> items = new List<MenuItem>(){
        new MenuItem(Guid.NewGuid(), "Burger", 4, 2.50),
        new MenuItem(Guid.NewGuid(), "Soda", 1, 1.50),
        new MenuItem(Guid.NewGuid(), "Fries", 2, 1.50)
    };

    [TestMethod]
    public void Creating_NewOrder_CalculatesTotalPrice_Correctly()
    {
        OrderModel result = new OrderModel(Guid.NewGuid(), items);
        double totalPrice = 0;
        items.ForEach(i => totalPrice += i.Price);

        Assert.IsNotNull(result.TotalPrice);
        Assert.AreEqual(result.TotalPrice, totalPrice);
        Assert.AreEqual(result.Items, items);
    }

    [TestMethod]
    public void Creating_NewOrderCalculates_TotalPrepTime_Correctly()
    {
        OrderModel result = new OrderModel(Guid.NewGuid(), items);

        double totalPrepTime = 0;
        items.ForEach(i => totalPrepTime += i.PreparationTime);

        Assert.IsNotNull(result.TotalPrepTime);
        Assert.AreEqual(result.TotalPrepTime, totalPrepTime);
    }
}
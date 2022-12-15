using Order.API.Services;
using Order.API.Tests.TestData;

namespace Order.API.Tests.Unit;

[TestClass]
public class OrderServiceTests
{
    private readonly IOrderService _service = new OrderService();
    [TestMethod]
    public void GetMenuItems_Returns_Default_List()
    {
        var result = _service.GetMenuItems();

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
        var postData = OrderTestData.GetTestOrderCreate();

        // act
        var result = _service.AddOrder(postData);

        // assert
        Assert.IsNotNull(result);
        Assert.AreEqual(postData.MenuItems.Count, result.Items.Count);
        Assert.IsInstanceOfType(result.Id, typeof(Guid));
    }
}

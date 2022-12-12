using Microsoft.AspNetCore.Mvc;
using Moq;
using Order.API.Services;
using Order.API.Controllers;
using Order.API.Contracts;
using Order.API.Models;

namespace Order.API.Test.Unit;

[TestClass]
public class OrderControllerTests
{
    [TestMethod]
    public void Get_OrdersMenuItems_Returns_ListOfMenuItems()
    {
        Mock<IKitchenService> mock = new Mock<IKitchenService>(MockBehavior.Strict);

        IKitchenService mockService = mock.Object;
        OrdersController controller = new OrdersController(mockService);

        var result = controller.GetMenuItems();

        var okObjectResult = result as OkObjectResult;

        Assert.IsNotNull(okObjectResult);
        Assert.IsInstanceOfType(okObjectResult, typeof(OkObjectResult));
        Assert.IsInstanceOfType(okObjectResult.Value, typeof(List<MenuItemResponse>));

        var resultAsList = okObjectResult.Value as List<MenuItemResponse>;

        Assert.IsNotNull(resultAsList);

        Assert.AreEqual("Burger", resultAsList[0].Name);
    }

    [TestMethod]
    public void Post_NewOrder_Returns_OrderHandled()
    {
        Mock<IKitchenService> mock = new Mock<IKitchenService>(MockBehavior.Strict);

        var testOrderHandeled = GetTestOrderHandled();

        mock.Setup(service => service.handleOrder(It.IsAny<OrderModel>())).Returns(testOrderHandeled);
        IKitchenService mockService = mock.Object;

        OrdersController controller = new OrdersController(mockService);

        var newOrder = new CreateOrderRequest(
            new List<string>(){
                "burger",
                "fries"
            }
        );

        var result = controller.PostOrder(newOrder);

        var okObjectResult = result as OkObjectResult;

        Assert.IsNotNull(okObjectResult);

        var resultValue = okObjectResult.Value as OrderHandled;

        Assert.IsNotNull(resultValue);

        Assert.AreEqual(resultValue.totalPrepTime, testOrderHandeled.totalPrepTime);
        Assert.AreEqual(resultValue.actualPrepTime, testOrderHandeled.actualPrepTime);
    }

    private OrderHandled GetTestOrderHandled()
    {
        return new OrderHandled(
            Guid.NewGuid(),
            7,
            5
        );
    }

    private List<MenuItem> GetTestListMenuItems()
    {
        return new List<MenuItem>(){
            new MenuItem(Guid.Parse("00000000-0000-0000-0000-000000000000"), "Burger", 5, 3.5),
            new MenuItem(Guid.Parse("00000000-0000-0000-0000-000000000000"), "Fries", 2, 2.5),
        };
    }
}
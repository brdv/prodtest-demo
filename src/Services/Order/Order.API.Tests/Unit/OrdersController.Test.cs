using Microsoft.AspNetCore.Mvc;
using Moq;
using Order.API.Services;
using Order.API.Controllers;
using Domain.Common.Contracts;
using Domain.Common.Models;
using Order.API.Tests.TestData;

namespace Order.API.Tests.Unit;

[TestClass]
public class OrderControllerTests
{
    [TestMethod]
    public void Get_OrdersMenuItems_Returns_ListOfMenuItems()
    {
        Mock<IOrderService> mock = new Mock<IOrderService>(MockBehavior.Strict);

        var testMenuItems = OrderTestData.GetTestListMenuItems();

        mock.Setup(service => service.GetMenuItems())
            .Returns(testMenuItems.Select(i => i.ToResponse()).ToList());

        IOrderService mockService = mock.Object;
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
    public void Post_AddOrder_Returns_OrderModel()
    {
        Mock<IOrderService> mock = new Mock<IOrderService>(MockBehavior.Strict);

        var testOrderModel = OrderTestData.GetTestOrderModel();

        mock.Setup(service => service.AddOrder(It.IsAny<CreateOrderRequest>())).Returns(testOrderModel);
        IOrderService mockService = mock.Object;

        OrdersController controller = new OrdersController(mockService);

        var newOrder = OrderTestData.GetTestOrderCreate();

        var result = controller.PostOrder(newOrder);

        var okObjectResult = result as OkObjectResult;

        Assert.IsNotNull(okObjectResult);

        var resultValue = okObjectResult.Value as OrderModel;

        Assert.IsNotNull(resultValue);
    }
}
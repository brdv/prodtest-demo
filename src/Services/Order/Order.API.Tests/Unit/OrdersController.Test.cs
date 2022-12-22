using Domain.Common.Contracts;
using Domain.Common.Generators;
using Domain.Common.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Order.API.Controllers;
using Order.API.Services;

namespace Order.API.Tests.Unit;

[TestClass]
public class OrderControllerTests
{
    [TestMethod]
    public void Get_OrdersMenuItems_Returns_ListOfMenuItems()
    {
        var mock = new Mock<IOrderService>(MockBehavior.Strict);

        var testMenuItems = TestDataGenerator.GetTestListMenuItems();

        mock.Setup(service => service.GetMenuItems())
            .Returns(testMenuItems.Select(i => i.ToResponse()).ToList());

        var mockService = mock.Object;
        var controller = new OrdersController(mockService);

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
        var mock = new Mock<IOrderService>(MockBehavior.Strict);

        var testOrderModel = TestDataGenerator.GetTestOrderModel();

        mock.Setup(service => service.AddOrder(It.IsAny<CreateOrderRequest>())).Returns(testOrderModel);
        var mockService = mock.Object;

        var controller = new OrdersController(mockService);

        var newOrder = TestDataGenerator.GetTestOrderCreate();

        var result = controller.PostOrder(newOrder);

        var okObjectResult = result as OkObjectResult;

        Assert.IsNotNull(okObjectResult);

        var resultValue = okObjectResult.Value as OrderModel;

        Assert.IsNotNull(resultValue);
    }
}

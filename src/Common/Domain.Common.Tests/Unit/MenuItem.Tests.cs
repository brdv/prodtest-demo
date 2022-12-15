using Domain.Common.Contracts;
using Domain.Common.Models;

namespace Domain.Common.Tests.Unit;

[TestClass]
public class MenuItemTests
{
    [TestMethod]
    public void Test_NewMenuItem()
    {
        var result = new MenuItem(Guid.NewGuid(), "test item", 2, 3);

        Assert.IsInstanceOfType(result, typeof(MenuItem));
        Assert.IsNotNull(result.Id);
        Assert.IsFalse(string.IsNullOrEmpty(result.Name));
    }

    [TestMethod]
    public void Test_MenuItemToResponse()
    {
        var item = new MenuItem(Guid.NewGuid(), "test item", 2, 3);

        var result = item.ToResponse();

        Assert.IsInstanceOfType(result, typeof(MenuItemResponse));
        Assert.IsFalse(String.IsNullOrEmpty(result.Name));
        Assert.IsNotNull(result.PreparationTime);
        Assert.IsNotNull(result.Price);
    }
}

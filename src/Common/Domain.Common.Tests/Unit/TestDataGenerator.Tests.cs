using Domain.Common.Generators;

namespace Domain.Common.Tests.Unit;

[TestClass]
public class Test_TestDataGenerators
{
    [TestMethod]
    public void Test_GetTestHandledOrder()
    {
        var result = TestDataGenerator.GetTestHandledOrder();

        Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-000000000000"), result.Id);
        Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-000000000000"), result.OrderId);
        Assert.IsNotNull(result.ActualPrepTime);
        Assert.IsNotNull(result.EstimatedPrepTime);
        Assert.IsFalse(string.IsNullOrEmpty(result.handler));
    }

    [TestMethod]
    public void Test_GetTestOrderModel()
    {
        var result = TestDataGenerator.GetTestOrderModel();
        var expectedItems = TestDataGenerator.GetTestListMenuItems();

        Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-000000000000"), result.Id);
        Assert.AreEqual(expectedItems.Count, result.Items.Count);
    }

    [TestMethod]
    public void Test_GetTestListMenuItems()
    {
        var result = TestDataGenerator.GetTestListMenuItems();

        foreach (var item in result)
        {
            Assert.AreEqual(Guid.Parse("00000000-0000-0000-0000-000000000000"), item.Id);
            Assert.IsFalse(string.IsNullOrEmpty(item.Name));
            Assert.IsInstanceOfType(item.Price, typeof(Double));
            Assert.IsNotNull(item.PreparationTime);
            Assert.IsNotNull(item.Price);
            Assert.IsInstanceOfType(item.PreparationTime, typeof(int));
        }
    }

    [TestMethod]
    public void Test_GetTestOrderCreate()
    {
        // Act
        var result = TestDataGenerator.GetTestOrderCreate();

        Assert.AreEqual(3, result.MenuItems.Count);
        foreach (var item in result.MenuItems)
        {
            Assert.IsFalse(string.IsNullOrEmpty(item));
        }
    }
}

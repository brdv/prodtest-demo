using Domain.Common.Contracts;
using Domain.Common.Models;

namespace Domain.Common.Generators;

public static class TestDataGenerator
{
    public static HandledOrder GetTestHandledOrder()
    {
        return new HandledOrder(
            Guid.Parse("00000000-0000-0000-0000-000000000000"),
            Guid.Parse("00000000-0000-0000-0000-000000000000"),
            5,
            6
        );
    }

    public static OrderModel GetTestOrderModel()
    {
        return new OrderModel(
            Guid.Parse("00000000-0000-0000-0000-000000000000"),
            GetTestListMenuItems()
        );
    }

    public static List<MenuItem> GetTestListMenuItems()
    {
        return new List<MenuItem>(){
            new MenuItem(Guid.Parse("00000000-0000-0000-0000-000000000000"), "Burger", 5, 3.5),
            new MenuItem(Guid.Parse("00000000-0000-0000-0000-000000000000"), "Fries", 2, 2.5),
        };
    }

    public static CreateOrderRequest GetTestOrderCreate()
    {
        return new CreateOrderRequest(
            new List<string>(){
                "burger",
                "fries",
                "soda"
            }
        );
    }
}

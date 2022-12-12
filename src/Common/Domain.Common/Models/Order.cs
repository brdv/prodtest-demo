namespace Domain.Common.Models;

public class OrderModel
{
    public Guid Id { get; }
    public List<MenuItem> Items { get; }
    public double TotalPrice { get; }
    public int TotalPrepTime { get; }

    public OrderModel(
      Guid id,
      List<MenuItem> items)
    {
        Id = id;
        Items = items;
        TotalPrepTime = GetTotalPrepTimeForOrder(items);
        TotalPrice = GetTotalPriceForOrder(items);
    }

    private static int GetTotalPrepTimeForOrder(List<MenuItem> items)
    {
        int totalPrepTime = 0;
        items.ForEach(i => totalPrepTime += i.PreparationTime);

        return totalPrepTime;
    }

    private static double GetTotalPriceForOrder(List<MenuItem> items)
    {
        double total = 0;
        items.ForEach(i => total += i.Price);

        return total;
    }
}
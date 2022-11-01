namespace ProdtestApi.Models;

public class MenuItem
{
    public Guid Id { get; }
    public string Name { get; }
    public int PreparationTime { get; }
    public double Price { get; }

    public MenuItem(
        Guid id,
        string name,
        int preparationTime,
        double price)
    {
        Id = id;
        Name = name;
        PreparationTime = preparationTime;
        Price = price;
    }
}
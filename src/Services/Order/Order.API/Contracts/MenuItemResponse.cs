namespace Order.API.Contracts;


public record MenuItemResponse(
    string Name,
    int PreparationTime,
    double Price
);
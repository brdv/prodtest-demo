namespace Domain.Common.Contracts;

public record MenuItemResponse(
    string Name,
    int PreparationTime,
    double Price
);
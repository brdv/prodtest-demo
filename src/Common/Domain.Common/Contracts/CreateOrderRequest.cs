namespace Domain.Common.Contracts;

public record CreateOrderRequest(
    List<string> MenuItems
);
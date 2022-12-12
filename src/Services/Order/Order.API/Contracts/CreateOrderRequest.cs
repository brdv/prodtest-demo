namespace Order.API.Contracts;

public record CreateOrderRequest(
    List<string> ItemIds
);
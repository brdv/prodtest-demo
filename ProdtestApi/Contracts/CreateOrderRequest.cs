namespace ProdtestApi.Contracts;

public record CreateOrderRequest(
    List<string> ItemIds
);
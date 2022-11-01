namespace ProdtestApi.Models;

public record OrderHandled(
    Guid orderId,
    int totalPrepTime,
    int actualPrepTime
);
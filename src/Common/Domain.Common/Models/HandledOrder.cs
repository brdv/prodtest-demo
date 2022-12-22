namespace Domain.Common.Models;

public record HandledOrder(
    Guid Id,
    Guid OrderId,
    int EstimatedPrepTime,
    int ActualPrepTime,
    string handler
);

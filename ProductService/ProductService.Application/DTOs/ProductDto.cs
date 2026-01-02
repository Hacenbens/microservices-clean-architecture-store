namespace ProductService.Application.DTOs;

public sealed record ProductDto(
    Guid Id,
    string Name,
    string Category,
    decimal Price,
    int StockQuantity
);
using MediatR;
using ProductService.Domain.Comman.Result;
using ProductService.Application.DTOs;

namespace ProductService.Application.Features.Products.Commands.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    string Category,
    decimal Price,
    int StockQuantity
): IRequest<Result<Guid>>;
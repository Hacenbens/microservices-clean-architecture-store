using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Comman.Result;
using ProductService.Application.DTOs;
using ProductService.Domain.Entities;
using MediatR;


namespace ProductService.Application.Features.Products.Queries;

public sealed class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, Result<List<ProductDto>>>
{
    private readonly IProductRepository _productRepository;

    public GetProductsByCategoryQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<List<ProductDto>>> Handle(
        GetProductsByCategoryQuery query,
        CancellationToken cancellationToken = default)
    {
        var products = await _productRepository.GetByCategoryAsync(
            query.Category,
            cancellationToken);

        if (products.Count == 0)
        {
            return Result<List<ProductDto>>.Failure(
                new(
                    "Product.Category.Empty",
                    $"No products found for category '{query.Category}'",
                    ErrorKind.NotFound));
        }

        var dtos = products.Select(p => new ProductDto(
            p.Id,
            p.Name,
            p.Category,
            p.Price,
            p.StockQuantity
        )).ToList();

        return Result<List<ProductDto>>.Success(dtos);
    }
}
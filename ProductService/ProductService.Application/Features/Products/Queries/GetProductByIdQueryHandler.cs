using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Comman.Result;
using ProductService.Application.DTOs;
using ProductService.Domain.Entities;
using ProductService.Application.Features.Products.Queries;
using MediatR;

namespace ProductService.Application.Features.Products.Queries;

public sealed class GetProductByIdQueryHandler: IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    private readonly IProductRepository _productRepository;

    public GetProductByIdQueryHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<ProductDto>> Handle(
        GetProductByIdQuery query,
        CancellationToken cancellationToken = default)
    {
        var product = await _productRepository.GetByIdAsync(
            query.ProductId,
            cancellationToken);

        if (product is null)
        {
            return Result<ProductDto>.Failure(
                new(
                    "Product.NotFound",
                    "Product not found",
                    ErrorKind.NotFound));
        }

        var dto = new ProductDto(
            product.Id,
            product.Name,
            product.Category,
            product.Price,
            product.StockQuantity);

        return Result<ProductDto>.Success(dto);
    }
}
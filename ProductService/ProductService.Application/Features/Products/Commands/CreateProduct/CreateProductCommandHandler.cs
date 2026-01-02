using MediatR;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Comman.Result;
using ProductService.Domain.Entities;

namespace ProductService.Application.Features.Products.Commands.CreateProduct;

public sealed class CreateProductCommandHandler
    : IRequestHandler<CreateProductCommand, Result<Guid>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result<Guid>> Handle(
        CreateProductCommand command,
        CancellationToken cancellationToken)
    {
        var createResult = Product.Create(
            command.Name,
            command.Category,
            command.Price,
            command.StockQuantity);

        if (!createResult.IsSuccess)
            return Result<Guid>.Failure(createResult.Error);

        var product = createResult.Value!;

        await _productRepository.AddAsync(product, cancellationToken);

        return Result<Guid>.Success(product.Id);
    }
}
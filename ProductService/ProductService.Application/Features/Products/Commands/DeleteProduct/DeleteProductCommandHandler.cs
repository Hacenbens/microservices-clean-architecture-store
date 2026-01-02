using MediatR;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Comman.Result;

namespace ProductService.Application.Features.Products.Commands.DeleteProduct;

public sealed class DeleteProductCommandHandler 
    : IRequestHandler<DeleteProductCommand, Result>
{
    private readonly IProductRepository _productRepository;

    public DeleteProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<Result> Handle(
        DeleteProductCommand command,
        CancellationToken cancellationToken)
    {
        var exists = await _productRepository.ExistsAsync(command.Id, cancellationToken);

        if (!exists)
        {
            return Result.Failure(new Error(
                "Product.NotFound",
                $"Product with ID '{command.Id}' does not exist",
                ErrorKind.NotFound));
        }

        var product = await _productRepository.GetByIdAsync(command.Id, cancellationToken);

        if (product is null)
        {
            return Result.Failure(new Error(
                "Product.NotFound",
                $"Product with ID '{command.Id}' does not exist",
                ErrorKind.NotFound));
        }

        await _productRepository.DeleteAsync(product, cancellationToken);

        return Result.Success();
    }
}
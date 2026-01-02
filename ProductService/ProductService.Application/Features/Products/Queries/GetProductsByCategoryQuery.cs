using MediatR;
using ProductService.Domain.Comman.Result;
using ProductService.Application.DTOs;

namespace ProductService.Application.Features.Products.Queries;

public sealed record GetProductsByCategoryQuery(string Category): IRequest<Result<List<ProductDto>>>;
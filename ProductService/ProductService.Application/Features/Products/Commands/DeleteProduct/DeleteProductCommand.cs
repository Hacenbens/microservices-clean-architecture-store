using ProductService.Domain.Comman.Result;
using ProductService.Application.DTOs;
using ProductService.Domain.Entities;
using MediatR;


namespace ProductService.Application.Features.Products.Commands.DeleteProduct;
public sealed record DeleteProductCommand(Guid Id) : IRequest<Result>;
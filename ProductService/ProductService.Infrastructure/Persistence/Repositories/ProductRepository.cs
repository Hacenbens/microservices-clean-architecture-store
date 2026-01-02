using Microsoft.EntityFrameworkCore;
using ProductService.Application.Abstractions.Repositories;
using ProductService.Domain.Entities;
using ProductService.Infrastructure.Persistence;

namespace ProductService.Infrastructure.Persistence.Repositories;

public class ProductRepository : IProductRepository
{
    private readonly ProductDbContext _context;

    public ProductRepository(ProductDbContext context)
    {
        _context = context;
    }

    public async Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<List<Product>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => ids.Contains(p.Id))
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .ToListAsync(cancellationToken);
    }

    public async Task<List<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .Where(p => p.Category == category)
            .ToListAsync(cancellationToken);
    }

    public async Task AddAsync(Product product, CancellationToken cancellationToken = default)
    {
        await _context.Products.AddAsync(product, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Update(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Product product, CancellationToken cancellationToken = default)
    {
        _context.Products.Remove(product);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Products
            .AnyAsync(p => p.Id == id, cancellationToken);
    }
}

using ProductService.Domain.Entities;

namespace ProductService.Application.Abstractions.Repositories;

public interface IProductRepository
{
    /// <summary>
    /// Get product by ID
    /// </summary>
    Task<Product?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get multiple products by IDs
    /// </summary>
    Task<List<Product>> GetByIdsAsync(List<Guid> ids, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get all products
    /// </summary>
    Task<List<Product>> GetAllAsync(CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Get products by category
    /// </summary>
    Task<List<Product>> GetByCategoryAsync(string category, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Add a product
    /// </summary>
    Task AddAsync(Product product, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Update a product
    /// </summary>
    Task UpdateAsync(Product product, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Delete a product
    /// </summary>
    Task DeleteAsync(Product product, CancellationToken cancellationToken = default);
    
    /// <summary>
    /// Check if product exists
    /// </summary>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
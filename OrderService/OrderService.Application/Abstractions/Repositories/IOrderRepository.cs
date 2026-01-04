using OrderService.Domain.Entities;

namespace OrderService.Application.Abstractions.Repositories;

public interface IOrderRepository
{
    /// <summary>
    /// Get order by ID
    /// </summary>
    Task<Order?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all orders for a specific user
    /// </summary>
    Task<List<Order>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default);

    /// <summary>
    /// Get all orders
    /// </summary>
    Task<List<Order>> GetAllAsync(CancellationToken cancellationToken = default);

    /// <summary>
    /// Add a new order
    /// </summary>
    Task AddAsync(Order order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Update an existing order
    /// </summary>
    Task UpdateAsync(Order order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Delete an order
    /// </summary>
    Task DeleteAsync(Order order, CancellationToken cancellationToken = default);

    /// <summary>
    /// Check if order exists
    /// </summary>
    Task<bool> ExistsAsync(Guid id, CancellationToken cancellationToken = default);
}
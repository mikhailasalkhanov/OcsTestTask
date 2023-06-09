using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Abstractions.Repositories;

public interface IOrderRepository
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order> CreateAsync(Order order);
    Task DeleteAsync(Order order);
    Task<Order> UpdateAsync(Order order);
}
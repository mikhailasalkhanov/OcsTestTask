using Ordering.Domain.Models;

namespace Ordering.Abstraction.Services;

public interface IOrderService
{
    Task<Order?> GetByIdAsync(Guid id);
    Task<Order?> CreateAsync(Order order);
    Task<Order?> UpdateAsync(Order order);
    Task<Order?> DeleteAsync(Guid id);
}
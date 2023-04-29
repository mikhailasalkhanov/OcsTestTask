using Ordering.Domain;

namespace Ordering.Application.Interfaces;

public interface IOrderService
{
    Task<Result<Order>> GetByIdAsync(Guid id);
    Task<Result<Order>> CreateAsync(Order order);
    Task<Result<Order>> UpdateAsync(Order order);
    Task<Result<Order>> DeleteAsync(Guid id);
}
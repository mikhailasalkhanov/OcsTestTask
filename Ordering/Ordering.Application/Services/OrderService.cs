using Ordering.Abstraction.Exceptions;
using Ordering.Abstraction.Services;
using Ordering.Domain.Models;
using Ordering.Infrastructure.Abstractions.Repositories;

namespace Ordering.Application.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _orderOrderRepository;

    public OrderService(IOrderRepository orderOrderRepository)
    {
        _orderOrderRepository = orderOrderRepository;
    }

    public async Task<Order?> GetByIdAsync(Guid id)
    {
        var order = await _orderOrderRepository.GetByIdAsync(id);
        if (order is not null && !order.IsDeleted)
        {
            return order;
        }
        
        return null;
    }

    public async Task<Order?> CreateAsync(Order order)
    {
        var sameOrderIsExist = await _orderOrderRepository.GetByIdAsync(order.Id) is not null;
        if (sameOrderIsExist)
        {
            return null;
        }

        if (IsNotAllLinesUnique(order.Lines))
        {
            throw new LineIsNotUnique("2 or more lines contain the same ProductIds");
        }

        return await _orderOrderRepository.CreateAsync(order);
    }
    
    public async Task<Order?> UpdateAsync(Order orderUpdation)
    {
        var orderToUpdate = await GetByIdAsync(orderUpdation.Id);
        if (orderToUpdate is null)
        {
            return null;
        }

        if (IsNotAllLinesUnique(orderUpdation.Lines))
        {
            throw new LineIsNotUnique("2 or more lines contain the same ProductIds");
        }
        
        if (!orderToUpdate.TryUpdateFrom(orderUpdation))
        {
            throw new OrderCannotBeModifiedException($"Order with status {orderToUpdate.Status} can't be edited");
        }

        return await _orderOrderRepository.UpdateAsync(orderToUpdate);
    }

    public async Task<Order?> DeleteAsync(Guid id)
    {
        var orderToDelete = await GetByIdAsync(id);
        if (orderToDelete is null)
        {
            return null;
        }

        if (!orderToDelete.CanBeDeleted())
        {
            throw new OrderCannotBeModifiedException($"Order with status {orderToDelete.Status} can't be deleted");
        }

        orderToDelete.IsDeleted = true;
        return await _orderOrderRepository.UpdateAsync(orderToDelete);
    }

    private bool IsNotAllLinesUnique(IEnumerable<OrderLine> lines)
    {
        var productIds = new HashSet<Guid>();
        return lines.Any(l => !productIds.Add(l.ProductId));
    }
}
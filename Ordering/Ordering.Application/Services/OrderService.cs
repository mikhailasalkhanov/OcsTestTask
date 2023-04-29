using Ordering.Application.Interfaces;
using Ordering.Domain;
using Ordering.Infrastructure.Interfaces;

namespace Ordering.Application.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _orderRepository;

    public OrderService(IRepository<Order> orderRepository)
    {
        _orderRepository = orderRepository;
    }

    public async Task<Result<Order>> GetByIdAsync(Guid id)
    {
        var order = await _orderRepository.GetByIdAsync(id);
        if (order is not null && !order.IsDeleted)
        {
            return Result<Order>.Success(order);
        }

        var message = $"Заказ id={id} не найден";
        return Result<Order>.Failure(message);
    }

    public async Task<Result<Order>> CreateAsync(Order order)
    {
        var sameOrderIsExist = await _orderRepository.GetByIdAsync(order.Id) is not null;
        if (sameOrderIsExist)
        {
            return Result<Order>.Failure($"Невозможно создать заказ с id={order.Id}");
        }

        var created = await _orderRepository.CreateAsync(order);
        return Result<Order>.Success(created);
    }
    
    public async Task<Result<Order>> UpdateAsync(Order order)
    {
        var searchResult = await GetByIdAsync(order.Id);
        if (!searchResult.IsSuccess)
        {
            return searchResult;
        }

        var orderToUpdate = searchResult.Value!;
        if (orderToUpdate.TryEditFrom(order))
        {
            var updated = await _orderRepository.UpdateAsync(orderToUpdate);
            return Result<Order>.Success(updated);
        }
        
        var result = Result<Order>.Failure($"Заказ со статусом {order.Status} не может быть изменен");
        result.Value = order;
        return result;
    }

    public async Task<Result<Order>> DeleteAsync(Guid id)
    {
        var searchResult = await GetByIdAsync(id);
        if (!searchResult.IsSuccess)
        {
            return searchResult;
        }

        var orderToDelete = searchResult.Value!;
        if (orderToDelete.CanBeDeleted())
        {
            orderToDelete.IsDeleted = true;
            var deleted = await _orderRepository.UpdateAsync(orderToDelete);
            return Result<Order>.Success(deleted);
        }
        
        var result = Result<Order>.Failure($"Заказ со статусом {orderToDelete.Status} не может быть удален");
        result.Value = orderToDelete;
        return result;
    }
}
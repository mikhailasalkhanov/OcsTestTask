using Microsoft.EntityFrameworkCore;
using Ordering.Domain;
using Ordering.Domain.Interfaces;
using Ordering.Domain.Models;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly ApplicationContext _context;

    public OrderRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            .Include(o => o.Lines)
            .FirstOrDefaultAsync(o => o.Id == id);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        var createdEntity = await _context.Orders.AddAsync(order);
        await _context.SaveChangesAsync();
        
        return createdEntity.Entity;
    }

    public async Task DeleteAsync(Order order)
    {
        _context.Orders.Remove(order);
        await _context.SaveChangesAsync();
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        var updated = _context.Update(order);
        
        await _context.SaveChangesAsync();
        
        return updated.Entity;
    }
}
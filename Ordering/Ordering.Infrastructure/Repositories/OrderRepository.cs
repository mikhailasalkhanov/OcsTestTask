using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Ordering.Domain;
using Ordering.Infrastructure.Interfaces;

namespace Ordering.Infrastructure.Repositories;

public class OrderRepository : IRepository<Order>
{
    private readonly ApplicationContext _context;

    public OrderRepository(ApplicationContext context)
    {
        _context = context;
    }
    
    public async Task<Order?> GetByIdAsync(Guid id)
    {
        return await _context.Orders
            //.AsNoTracking()
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

    public async Task<IEnumerable<Order>> GetAsync(Expression<Func<Order, bool>> filter)
    {
        return await _context.Set<Order>()
            .Where(filter)
            .AsNoTracking()
            .ToListAsync();
    }
}
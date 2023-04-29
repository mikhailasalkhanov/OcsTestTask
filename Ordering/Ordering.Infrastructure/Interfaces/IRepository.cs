using System.Linq.Expressions;

namespace Ordering.Infrastructure.Interfaces;

public interface IRepository<T> where T : class
{
    Task<T?> GetByIdAsync(Guid id);
    Task<T> CreateAsync(T order);
    Task DeleteAsync(T order);
    Task<T> UpdateAsync(T order);
    Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter);
}
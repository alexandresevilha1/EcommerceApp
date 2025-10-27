using System.Linq.Expressions;

namespace EcommerceApp.Core.Repositories;

public interface IRepository<T>
{
    Task <IEnumerable<T>> GetAllAsync();
    Task<T?> GetAsync(Expression<Func<T, bool>> predicate);
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    T Update(T entity);
    T Delete(T entity);
}

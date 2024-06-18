using System.Linq.Expressions;

namespace dotNet_RESTful_Web_API.Repository.IRepository;

public interface IRepository<T> where T: class
{
    Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
        int pageSize = 0, int pageNumber = 1);
    Task<T?> GetAsync(Expression<Func<T,bool>>? filter= null, bool tracked = true);
    Task CreateAsync(T entity);
    Task RemoveAsync(T entity);
    Task SaveAsync();
}
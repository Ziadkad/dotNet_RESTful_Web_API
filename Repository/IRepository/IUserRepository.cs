using System.Linq.Expressions;
using dotNet_RESTful_Web_API.models;

namespace dotNet_RESTful_Web_API.Repository.IRepository;

public interface IUserRepository
{
    Task<List<User>?> GetAllAsync(Expression<Func<User,bool>>? filter = null);
    Task<User?> GetAsync(Expression<Func<User,bool>>? filter= null, bool tracked = true);
    Task CreateAsync(User entity);
    Task RemoveAsync(User entity);
    Task UpdateAsync(User entity);
    Task SaveAsync();

}
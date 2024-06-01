using System.Linq.Expressions;
using dotNet_RESTful_Web_API.models;

namespace dotNet_RESTful_Web_API.Repository.IRepository;

public interface IUserRepository
{
    Task<List<User>> GetAll(Expression<Func<User,bool>> filter = null);
    Task<User> Get(Expression<Func<User,bool>>? filter, bool tracked = true);
    Task Create(User entity);
    Task Remove(User entity);
    Task Save();
}
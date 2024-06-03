using System.Linq.Expressions;
using dotNet_RESTful_Web_API.models;

namespace dotNet_RESTful_Web_API.Repository.IRepository;

public interface IUserRepository : IRepository<User>
{
    Task<User> UpdateAsync(User entity);
}
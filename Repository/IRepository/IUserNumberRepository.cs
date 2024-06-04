using dotNet_RESTful_Web_API.models;

namespace dotNet_RESTful_Web_API.Repository.IRepository;

public interface IUserNumberRepository : IRepository<UserNumber>
{
    Task<UserNumber> UpdateAsync(UserNumber entity);
}
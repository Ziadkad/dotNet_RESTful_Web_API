using System.Linq.Expressions;
using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_RESTful_Web_API.Repository;

public class UserRepository : Repository<User>, IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<User> UpdateAsync(User entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _db.Users.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
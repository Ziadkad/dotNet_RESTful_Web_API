using System.Linq.Expressions;
using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_RESTful_Web_API.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _db;

    public UserRepository(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<User>> GetAllAsync(Expression<Func<User,bool>> filter)
    {
        IQueryable<User> query = _db.Users;
        if(filter != null){ query = query.Where(filter);}
        return await query.ToListAsync();
    }

    public async Task<User> GetAsync(Expression<Func<User,bool>> filter, bool tracked = true)
    {
        IQueryable<User> query = _db.Users;
        if (!tracked) {  query = query.AsNoTracking();}
        if(filter != null){ query = query.Where(filter);}
        return await query.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(User entity)
    {
        await _db.Users.AddAsync(entity);
        await SaveAsync();
    }

    public async Task RemoveAsync(User entity)
    {
        _db.Users.Remove(entity);
        await SaveAsync();
    }

    public async Task UpdateAsync(User entity)
    {
        _db.Users.Update(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
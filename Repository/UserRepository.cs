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

    public async Task<List<User>> GetAll(Expression<Func<User,bool>> filter)
    {
        IQueryable<User> query = _db.Users;
        if(filter != null){query.Where(filter);}
        return await query.ToListAsync();
    }

    public async Task<User> Get(Expression<Func<User,bool>>? filter, bool tracked = true)
    {
        IQueryable<User> query = _db.Users;
        if (!tracked) { query.AsNoTracking();}
        if(filter != null){query.Where(filter);}
        return await query.FirstOrDefaultAsync();
    }

    public async Task Create(User entity)
    {
        await _db.Users.AddAsync(entity);
        await Save();
    }

    public async Task Remove(User entity)
    {
        _db.Users.Remove(entity);
        await Save();
    }

    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}
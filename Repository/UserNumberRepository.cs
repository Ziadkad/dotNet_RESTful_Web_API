using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.models;
using dotNet_RESTful_Web_API.Repository.IRepository;

namespace dotNet_RESTful_Web_API.Repository;

public class UserNumberRepository : Repository<UserNumber>, IUserNumberRepository
{
    private readonly AppDbContext _db;
    public UserNumberRepository(AppDbContext db) : base(db)
    {
        _db = db;
    }

    public async Task<UserNumber> UpdateAsync(UserNumber entity)
    {
        entity.UpdatedDate = DateTime.Now;
        _db.UserNumbers.Update(entity);
        await _db.SaveChangesAsync();
        return entity;
    }
}
using System.Linq.Expressions;
using dotNet_RESTful_Web_API.Data;
using dotNet_RESTful_Web_API.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace dotNet_RESTful_Web_API.Repository;

public class Repository<T>: IRepository<T> where T: class
{
    private readonly AppDbContext _db;
    internal DbSet<T> DbSet;
    public Repository ( AppDbContext db)
    {
        _db = db;
        this.DbSet = _db.Set<T>();
    }

    public async Task<List<T>?> GetAllAsync(Expression<Func<T, bool>>? filter = null, string? includeProperties = null,
        int pageSize = 0, int pageNumber = 1)
    {
        IQueryable<T> query = DbSet;
        if(filter != null){ query = query.Where(filter);}
        if (pageSize > 0)
        {
            if (pageSize > 100)
            {
                pageSize = 100;
            }
            //skip0.take(5)
            //page number- 2     || page size -5
            //skip(5*(1)) take(5)
            query = query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
        }
        if (includeProperties != null)
        {
            foreach (var includeProp in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProp);
            }
        }
        return await query.ToListAsync();
    }

    public async Task<T?> GetAsync(Expression<Func<T,bool>>? filter, bool tracked = true)
    {
        IQueryable<T> query = DbSet;
        if (!tracked) {  query = query.AsNoTracking();}
        if(filter != null){ query = query.Where(filter);}
        return await query.FirstOrDefaultAsync();
    }

    public async Task CreateAsync(T entity)
    {
        await DbSet.AddAsync(entity);
        await SaveAsync();
    }

    public async Task RemoveAsync(T entity)
    {
        DbSet.Remove(entity);
        await SaveAsync();
    }

    public async Task SaveAsync()
    {
        await _db.SaveChangesAsync();
    }
}
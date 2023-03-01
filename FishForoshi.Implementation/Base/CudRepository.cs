using FishForoshi.Abstraction;
using FishForoshi.Database;
using FishForoshi.Entities;
using Microsoft.EntityFrameworkCore;

namespace FishForoshi.Implementation;
public class CudRepository<T> : ICudRepository<T> where T : BaseEntity
{

    private readonly FishForoshiContext _context;
    private readonly DbSet<T> _dbset;

    public CudRepository(FishForoshiContext context)
    {
        _context = context;
        _dbset = context.Set<T>();
    }

    public async Task<bool> AddAsync(T entity)
    {
        try
        {
            await _dbset.AddAsync(entity);
            return await SaveAsync();
        }
        catch
        {

            return false;
        }
    }

    public async Task<bool> AddRangeAsync(IEnumerable<T> entity)
    {
        try
        {
            await _dbset.AddRangeAsync(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateAsync(T entity)
    {
        try
        {
            _dbset.Update(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> UpdateRangeAsync(IEnumerable<T> entity)
    {
        try
        {
            _dbset.UpdateRange(entity);
            return await SaveAsync();
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteByIdAsync(Guid Guid)
    {
        try
        {
            var entity = await _dbset.FindAsync(Guid);
            entity.IsDeleted = true;
            entity.DeleteDateTime = DateTime.Now;
            return await UpdateAsync(entity);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> DeleteAsync(T entity)
    {
        try
        {
            entity.IsDeleted = true;
            entity.DeleteDateTime = DateTime.Now;
            return await UpdateAsync(entity);
        }
        catch
        {
            return false;
        }
    }

    public async Task<bool> SaveAsync()
    {
        await _context.SaveChangesAsync();
        return true;
    }
}


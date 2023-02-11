using FishForoshi.Abstraction;
using FishForoshi.Database;
using FishForoshi.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Infrastructure.Implementation.Dal
{
    public class QueryRepository<T> : IQueryRepository<T> where T : BaseEntity
    {
        private readonly FishForoshiContext _context;
        private readonly DbSet<T> _dbset;

        public QueryRepository(FishForoshiContext context)
        {
            _context = context;
            _dbset = context.Set<T>();
        }

        public async Task<IEnumerable<T>> GetAllAsync()
            => await _dbset.ToListAsync();

        public async Task<IEnumerable<T>> GetAllAsync(Expression<Func<T, bool>> where)
            => await _dbset.Where(where).ToListAsync();

        public async Task<T> GetAsync(object? id)
            => await _dbset.FindAsync(id);

    }
}

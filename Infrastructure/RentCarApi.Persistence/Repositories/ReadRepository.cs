using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using RentCarApi.Application.Repositories;
using RentCarApi.Domain.Models.Base;
using RentCarApi.Persistence.Context;
using System.Linq.Expressions;

namespace RentCarApi.Persistance.Repositories
{
    public class ReadRepository<T> : IReadRepository<T> where T : class, IBaseEntity, new()
    {
        private readonly AppDbContext _context;

        public ReadRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();

        public async Task<IQueryable<T>> GetAll(bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = query.AsNoTracking();
            if (include != null)
            {
                query = include(query);
            }
            return query;
        }
        public async Task<IQueryable<T>> GetWhere(Expression<Func<T, bool>> method, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            var query = Table.Where(method);
            if (!tracking)
                query = query.AsNoTracking();
            if (include != null)
            {
                query = include(query);
            }
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return query;
        }
        public async Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
        {
            var query = Table.AsQueryable();
            if (!tracking)
                query = Table.AsNoTracking();
            return await query.FirstOrDefaultAsync(method);
        }
        public async Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null)
        {
            IQueryable<T> query = Table.Where(predicate);

            if (include != null)
            {
                query = include(query);
            }
            if (!tracking)
            {
                query = query.AsNoTracking();
            }
            return await query.FirstOrDefaultAsync();
        }
    }
}
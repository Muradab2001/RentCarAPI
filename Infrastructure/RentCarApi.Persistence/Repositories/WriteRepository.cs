using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using RentCarApi.Application.Repositories;
using RentCarApi.Domain.Models.Base;
using RentCarApi.Persistence.Context;


namespace SilkPlasterApi.Persistance.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : class, IBaseEntity, new()
    {
        readonly private AppDbContext _context;
        public WriteRepository(AppDbContext context)
        {
            _context = context;
        }

        public DbSet<T> Table => _context.Set<T>();
        public async Task<bool> AddAsync(T model)
        {
            EntityEntry<T> entityEntry = await Table.AddAsync(model);
            return entityEntry.State == EntityState.Added;
        }
        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }
        public bool Remove(T model)
        {
            EntityEntry<T> entityEntry = Table.Remove(model);
            return entityEntry.State == EntityState.Deleted;
        }
        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }
        public async Task RemoveAsync(T entity)
        {
            Table.Remove(entity);
        }
        public bool Update(T model)
        {
            EntityEntry entityEntry = Table.Update(model);
            return entityEntry.State == EntityState.Modified;
        }      
    }
}

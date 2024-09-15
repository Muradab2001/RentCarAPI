using RentCarApi.Application.Repositories;
using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Application.UnitOfWork;
public interface IUnitOfWork
{
    IWriteRepository<T> GetWriteRepository<T>() where T : class, IBaseEntity, new();
    IReadRepository<T> GetReadRepository<T>() where T : class, IBaseEntity, new();
    Task<int> SaveChangesAsync();
    int SaveChanges();
    Task BeginTransactionAsync();
    Task CommitTransactionAsync();
    Task RollbackTransactionAsync();
}
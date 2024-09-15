using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using RentCarApi.Application.Repositories;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models.Base;
using RentCarApi.Persistance.Repositories;
using RentCarApi.Persistence.Context;
using SilkPlasterApi.Persistance.Repositories;

namespace RentCarApi.Persistence.UnitOfWork;

public class UnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;
    private IDbContextTransaction _transaction;
    public UnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task BeginTransactionAsync()=> _transaction = await _dbContext.Database.BeginTransactionAsync();
    public async Task CommitTransactionAsync() => await _transaction.CommitAsync();
    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
    }
    //public async ValueTask DisposeAsync() => await _dbContext.DisposeAsync();
    public int SaveChanges() => _dbContext.SaveChanges();
    public async Task<int> SaveChangesAsync() => await _dbContext.SaveChangesAsync();
    IReadRepository<T> IUnitOfWork.GetReadRepository<T>() => new ReadRepository<T>(_dbContext);
    IWriteRepository<T> IUnitOfWork.GetWriteRepository<T>() => new WriteRepository<T>(_dbContext);
}
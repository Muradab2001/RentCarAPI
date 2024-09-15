using Microsoft.EntityFrameworkCore.Query;
using RentCarApi.Domain.Models.Base;
using System.Linq.Expressions;

namespace RentCarApi.Application.Repositories;

public interface IReadRepository<T> : IRepository<T> where T : class, IBaseEntity, new()
{
    Task<IQueryable<T>> GetAll(bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    Task<IQueryable<T>> GetWhere(Expression<Func<T, bool>> method, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
    Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true);
    Task<T> GetAsync(Expression<Func<T, bool>> predicate, bool tracking = true, Func<IQueryable<T>, IIncludableQueryable<T, object>> include = null);
}

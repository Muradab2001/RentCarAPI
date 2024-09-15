using RentCarApi.Domain.Models.Base;
namespace RentCarApi.Application.Repositories
{
    public interface IWriteRepository<T> : IRepository<T> where T : class, IBaseEntity, new()
    {
        Task<bool> AddAsync(T model);
        Task<bool> AddRangeAsync(List<T> datas);     
        bool RemoveRange(List<T> datas);
        Task RemoveAsync(T model);
        bool Update(T model);
    }
} 

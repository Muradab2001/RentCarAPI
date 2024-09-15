using Microsoft.EntityFrameworkCore;
using RentCarApi.Domain.Models.Base;

namespace RentCarApi.Application.Repositories
{
    public interface IRepository<T> where T : class, IBaseEntity , new()
    {
        DbSet<T> Table { get; }
    }
}

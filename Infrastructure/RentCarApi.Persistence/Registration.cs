using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentCarApi.Application.Repositories;
using RentCarApi.Application.UnitOfWork;
using RentCarApi.Domain.Models;
using RentCarApi.Persistance.Repositories;
using RentCarApi.Persistence.Context;
using SilkPlasterApi.Persistance.Repositories;

namespace RentCarApi.Persistence;
public static class Registration
{
    public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options => options
        .UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
        services.AddIdentity<AppUser, AppRole>(opt =>
        {
            opt.Password.RequireNonAlphanumeric = false;
            opt.User.RequireUniqueEmail = false;
            opt.Password.RequireUppercase = false;
            opt.Password.RequireLowercase = false;
            opt.Password.RequiredLength = 8;
            opt.Lockout.MaxFailedAccessAttempts = 5;
        })
        .AddEntityFrameworkStores<AppDbContext>()
        .AddSignInManager<SignInManager<AppUser>>()
        .AddDefaultTokenProviders();
        services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
        services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
        services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork.UnitOfWork));
    }
}
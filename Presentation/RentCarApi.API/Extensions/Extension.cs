using Microsoft.AspNetCore.Identity;
using RentCarApi.Domain.Models;
using RentCarApi.Persistence.Context;

namespace RentCarApi.API.Extensions;
public static class Extension
{
    public async static Task CreateRoleAsync(this IServiceScope serviceScope)
    {
        var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<AppRole>>();
        var roles = new[] { "Company", "Admin", "User" };
        foreach (var role in roles)
        {
            if (!await roleManager.RoleExistsAsync(role))
                await roleManager.CreateAsync(new AppRole() { Name = role });
        }
    }
}

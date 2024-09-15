using AutoMapper;
using FluentValidation.AspNetCore;
using Hangfire;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RentCarApi.Application.Mapping;
using RentCarApi.Application.Middleware;
using System.Reflection;

namespace RentCarApi.Application;

public static class Registration
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
        var mapperConfiguration = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new MapperProfile());
        });
        services.AddSingleton(mapperConfiguration.CreateMapper());
        services.AddTransient<GlobalExceptionHandler>();
        services.AddControllers().AddFluentValidation(p => p.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));
        return services;
    }
}
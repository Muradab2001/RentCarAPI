using Hangfire;
using Hangfire.SqlServer;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using RentCarApi.Application.Common.Interfaces.Authentication;
using RentCarApi.Application.Common.Interfaces.BackgroundJob;
using RentCarApi.Application.Common.Interfaces.Email;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.Services;
using RentCarApi.Infrastructure.Authentication;
using RentCarApi.Infrastructure.Services;
using System.Security.Claims;
using System.Text;
using System.Threading.RateLimiting;

namespace RentCarApi.Infrastructure;
public static class Registration
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton<IStorageService, GoogleCloudStorageService>();
        services.AddScoped<ITokenGeneratorService, TokenGeneratorService>();
        services.AddScoped<IEmailManager, EmailManager>();
        services.AddScoped<IRentReminderService, RentReminderService>();
        services.AddHttpContextAccessor();

        services.AddHangfire(config => config
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(configuration.GetConnectionString("DefaultConnection"), new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

        var jwtSettings = configuration.GetSection("JwtSettings").Get<JwtSettings>();
        services.AddSingleton(jwtSettings);
        var emailSettings = configuration.GetSection("EmailSettings").Get<EmailSettings>();
        services.AddSingleton(emailSettings);


        TokenValidationParameters tokenValidationParameters = new()
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
            ValidateIssuer = true,
            ValidateAudience = true,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero,
            ValidIssuer = jwtSettings.Issuer,
            ValidAudience = jwtSettings.Audience,
            LifetimeValidator = (notBefore, expires, securityToken, validationParameters) => expires != null ? expires > DateTime.UtcNow : false,
            NameClaimType = ClaimTypes.Name
        };
        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(opt =>
        {
            opt.SaveToken = true;
            opt.TokenValidationParameters = tokenValidationParameters;

            opt.Events = new JwtBearerEvents
            {
                OnMessageReceived = context =>
                {
                    var accessToken = context.Request.Query["access_token"];
                    var path = context.HttpContext.Request.Path;
                    if (!string.IsNullOrEmpty(accessToken) && path.StartsWithSegments("/orders-hub"))
                    {
                        context.Token = accessToken;
                    }
                    return Task.CompletedTask;
                }
            };
        });
        services.AddRateLimiter(options =>
        {
            options.AddFixedWindowLimiter("Basic", _options =>
            {
                _options.Window = TimeSpan.FromSeconds(12);
                _options.PermitLimit = 6;
                _options.QueueLimit = 2;
                _options.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
            });
        });
        return services;
    }
}
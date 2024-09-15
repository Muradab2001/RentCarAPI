using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using RentCarApi.Application.Helpers;
using RentCarApi.Application.Middleware;

namespace RentCarApi.API
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddWebApiDI(this IServiceCollection services, ConfigureHostBuilder host)
        {
            services.AddRouting(x => x.LowercaseUrls = true);

            services.AddApiVersioningAndApiExplorer();

            services.AddRouting(x => x.LowercaseUrls = false);
            services.AddTransient<GlobalExceptionHandler>();
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
            services.AddSwaggerGen(opt =>
            {
                var provider = services.BuildServiceProvider()
                                       .GetRequiredService<IApiVersionDescriptionProvider>();

                foreach (var description in provider.ApiVersionDescriptions)
                {
                    opt.SwaggerDoc(description.GroupName, new OpenApiInfo
                    {
                        Title = $"Airbnb API {description.ApiVersion}",
                        Version = description.ApiVersion.ToString()
                    });
                }

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Description = "Bearer Authentication with JWT Token",
                    Type = SecuritySchemeType.Http
                });

                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
             {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Id = "Bearer",
                        Type = ReferenceType.SecurityScheme
                    }
                },
                new List<string>()
             }
        });

                opt.MapType<TimeSpan>(() => new OpenApiSchema
                {
                    Type = "string",
                    Example = new OpenApiString(string.Empty)
                });
            });

            services.AddHttpClient();

            return services;
        }
        public static IServiceCollection AddApiVersioningAndApiExplorer(this IServiceCollection services)
        {
            services.AddApiVersioning(opt =>
            {
                opt.ReportApiVersions = true;
                opt.AssumeDefaultVersionWhenUnspecified = true;
                opt.DefaultApiVersion = new ApiVersion(1, 0);
                opt.ApiVersionReader = new UrlSegmentApiVersionReader();
            })
            .AddVersionedApiExplorer(opt => opt.GroupNameFormat = "'v'VVV");
            return services;
        }

    }
}
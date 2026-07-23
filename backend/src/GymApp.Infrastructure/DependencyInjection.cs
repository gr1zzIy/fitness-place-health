using GymApp.Application.Common.Interfaces;
using GymApp.Infrastructure.Identity;
using GymApp.Infrastructure.Persistence;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GymApp.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        // для токенів Identity
        services.AddDataProtection();
        
        // БД підключення
        var connectionString = configuration.GetConnectionString(
            "Database");

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                "Connection string 'Database' was not found.");
        }

        services.AddDbContext<AppDbContext>(options =>
        {
            options
                .UseNpgsql(connectionString)
                .UseSnakeCaseNamingConvention();
        });

        services.AddScoped<IAppDbContext>(
            provider => provider.GetRequiredService<AppDbContext>());

        // Ролі
        
        services
            .AddIdentityCore<ApplicationUser>(options =>
            {
                options.User.RequireUniqueEmail = true;

                options.Password.RequiredLength = 8;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = false;
            })
            .AddRoles<IdentityRole<Guid>>()
            .AddEntityFrameworkStores<AppDbContext>()
            .AddDefaultTokenProviders();
        
        // Для проброса так як не можна юзати UserManager<ApplicationUser> напряму в Application Layer
        services.AddScoped<IIdentityService, IdentityService>();
        
        return services;
    }
}
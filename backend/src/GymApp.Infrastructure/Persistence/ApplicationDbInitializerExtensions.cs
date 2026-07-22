using GymApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace GymApp.Infrastructure.Persistence;

public static class ApplicationDbInitializerExtensions
{
    public static async Task SeedIdentityDataAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();

        var roleManager = scope.ServiceProvider
            .GetRequiredService<RoleManager<IdentityRole<Guid>>>();

        await IdentityDataSeeder.SeedRolesAsync(roleManager);
    }
}
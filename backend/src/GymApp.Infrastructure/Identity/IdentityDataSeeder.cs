using GymApp.Domain.Constants;
using Microsoft.AspNetCore.Identity;

namespace GymApp.Infrastructure.Identity;

public static class IdentityDataSeeder
{
    public static async Task SeedRolesAsync(RoleManager<IdentityRole<Guid>> roleManager)
    {
        string[] roles =
        [
            AppRoles.Admin,
            AppRoles.Trainer,
            AppRoles.Client,
            AppRoles.Receptionist
        ];

        foreach (var roleName in roles)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<Guid>
                {
                    Name = roleName,
                    NormalizedName = roleName.ToUpperInvariant()
                });
            }
        }
    }
}
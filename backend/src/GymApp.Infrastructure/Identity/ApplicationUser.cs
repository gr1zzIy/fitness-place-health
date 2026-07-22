using Microsoft.AspNetCore.Identity;

namespace GymApp.Infrastructure.Identity;

public sealed class ApplicationUser : IdentityUser<Guid>
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public DateTimeOffset? DateOfBirth { get; set; }

    public bool IsActive { get; set; } = true;

    public DateTimeOffset CreatedAt { get; set; }
    public DateTimeOffset? UpdatedAt { get; set; }
}
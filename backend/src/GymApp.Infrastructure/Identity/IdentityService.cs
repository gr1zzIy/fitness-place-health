using GymApp.Application.Common.Interfaces;
using GymApp.Domain.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Infrastructure.Identity;

public class IdentityService : IIdentityService
{
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public async Task<bool> EmailExistsAsync(string email, CancellationToken ct = default)
    {
        return await _userManager.FindByEmailAsync(email) != null;
    }

    public async Task<(Guid UserId, IDictionary<string, string[]> Errors)> CreateUserAsync(
        string email,
        string password,
        string firstName,
        string lastName,
        DateTimeOffset? dateOfBirth,
        string role,
        CancellationToken ct = default)
    {
        var user = new ApplicationUser
        {
            Id = Guid.NewGuid(),
            UserName = email,
            Email = email,
            FirstName = firstName,
            LastName = lastName,
            DateOfBirth = dateOfBirth,
            CreatedAt = DateTimeOffset.UtcNow,
            IsActive = true
        };

        var createResult = await _userManager.CreateAsync(user, password);

        if (!createResult.Succeeded)
        {
            var errors = createResult.Errors
                .GroupBy(e => e.Code)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.Description).ToArray()
                );

            return (Guid.Empty, errors);
        }

        var roleResult = await _userManager.AddToRoleAsync(user, role);
        if (!roleResult.Succeeded)
        {
            throw new DomainException("Не вдалося призначити роль для користувача.");
        }

        return (user.Id, new Dictionary<string, string[]>());
    }

    public async Task<UserDto?> GetUserByIdAsync(Guid userId, CancellationToken ct = default)
    {
        var user = await _userManager.FindByIdAsync(userId.ToString());
        return user == null ? null : new UserDto(user.Id, user.FirstName, user.LastName, user.Email!);
    }

    public async Task<Dictionary<Guid, UserDto>> GetUsersByIdsAsync(IEnumerable<Guid> userIds, CancellationToken ct = default)
    {
        var ids = userIds.ToList();
        return await _userManager.Users
            .AsNoTracking()
            .Where(u => ids.Contains(u.Id))
            .ToDictionaryAsync(
                u => u.Id,
                u => new UserDto(u.Id, u.FirstName, u.LastName, u.Email!),
                ct);
    }
}
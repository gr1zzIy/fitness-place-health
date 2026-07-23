namespace GymApp.Application.Common.Interfaces;

public record UserDto(
    Guid Id,
    string FirstName,
    string LastName,
    string Email
);

public interface IIdentityService
{
    Task<bool> EmailExistsAsync(string email, CancellationToken ct = default);

    Task<(Guid UserId, IDictionary<string, string[]> Errors)> CreateUserAsync(
        string email, 
        string password, 
        string firstName, 
        string lastName, 
        DateTimeOffset? dateOfBirth, 
        string role, 
        CancellationToken ct = default);

    Task<UserDto?> GetUserByIdAsync(Guid userId, CancellationToken ct = default);

    Task<Dictionary<Guid, UserDto>> GetUsersByIdsAsync(IEnumerable<Guid> userIds, CancellationToken ct = default);
}
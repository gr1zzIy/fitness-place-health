namespace GymApp.Application.Gyms.Queries.DTOs;

public record GymDto(
    Guid Id, 
    string Name, 
    string? Description, 
    string TimeZone, 
    bool IsActive, 
    DateTimeOffset CreatedAt, 
    List<GymBranchDto> Branches);
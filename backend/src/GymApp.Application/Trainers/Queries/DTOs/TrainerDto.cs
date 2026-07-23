namespace GymApp.Application.Trainers.Queries.DTOs;

public record SpecializationDto(
    Guid Id, 
    string Name, 
    string? Description);

public record TrainerDto(
    Guid Id,
    Guid UserId,
    string FirstName,
    string LastName,
    string Email,
    string? Bio,
    int ExperienceYears,
    bool IsAvailable,
    List<SpecializationDto> Specializations
);
namespace GymApp.Application.Trainers.Commands.CreateTrainer;

public record CreateTrainerCommand(
    string Email,
    string Password,
    string FirstName,
    string LastName,
    DateTimeOffset? DateOfBirth,
    string? Bio,
    int ExperienceYears,
    List<Guid>? SpecializationIds
);
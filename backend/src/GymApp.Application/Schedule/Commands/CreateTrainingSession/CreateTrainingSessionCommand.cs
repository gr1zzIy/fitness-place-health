namespace GymApp.Application.Schedule.Commands;

public record CreateTrainingSessionCommand(
    Guid GymBranchId,
    Guid GymRoomId,
    Guid TrainingTypeId,
    Guid TrainerProfileId,
    DateTimeOffset StartsAt,
    DateTimeOffset EndsAt,
    int Capacity
);
namespace GymApp.Application.Trainers.Commands.AddTrainerSpecialization;

public record AddTrainerSpecializationCommand(
    Guid TrainerProfileId, 
    Guid SpecializationId);
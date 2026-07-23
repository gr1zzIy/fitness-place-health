using GymApp.Application.Trainers.Commands.AddTrainerSpecialization;
using GymApp.Application.Trainers.Commands.CreateTrainer;
using GymApp.Application.Trainers.Queries.DTOs;

namespace GymApp.Application.Trainers;

public interface ITrainerService
{
    Task<Guid> CreateTrainerAsync(CreateTrainerCommand command, CancellationToken ct = default);
    
    Task AddSpecializationAsync(AddTrainerSpecializationCommand command, CancellationToken ct = default);
    
    Task<TrainerDto> GetByIdAsync(Guid trainerProfileId, CancellationToken ct = default);
    
    Task<IReadOnlyCollection<TrainerDto>> GetAllAsync(CancellationToken ct = default);
}
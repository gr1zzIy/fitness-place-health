using GymApp.Application.Schedule.Commands;

namespace GymApp.Application.Schedule;

public interface ITrainingSessionService
{
    Task<Guid> CreateSessionAsync(CreateTrainingSessionCommand command, CancellationToken ct = default);
}
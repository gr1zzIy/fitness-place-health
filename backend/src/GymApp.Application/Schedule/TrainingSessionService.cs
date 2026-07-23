using FluentValidation;
using GymApp.Application.Common.Extensions;
using GymApp.Application.Common.Interfaces;
using GymApp.Application.Schedule.Commands;
using GymApp.Domain.Schedule;

namespace GymApp.Application.Schedule;

public sealed class TrainingSessionService : ITrainingSessionService
{
    private readonly IAppDbContext _context;
    private readonly IValidator<CreateTrainingSessionCommand> _validator;

    public TrainingSessionService(
        IAppDbContext context,
        IValidator<CreateTrainingSessionCommand> validator)
    {
        _context = context;
        _validator = validator;
    }

    public async Task<Guid> CreateSessionAsync(CreateTrainingSessionCommand command, CancellationToken ct = default)
    {
        // FluentValidatio
        await _validator.ValidateAndThrowDomainAsync(command, ct);

        // Створення через фабрику
        var session = TrainingSession.Create(
            command.GymBranchId,
            command.GymRoomId,
            command.TrainingTypeId,
            command.TrainerProfileId,
            command.StartsAt,
            command.EndsAt,
            command.Capacity
        );

        // Збереження в БД
        _context.TrainingSessions.Add(session);
        await _context.SaveChangesAsync(ct);

        return session.Id;
    }
}
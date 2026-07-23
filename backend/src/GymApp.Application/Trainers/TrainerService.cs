using FluentValidation;
using GymApp.Application.Common.Interfaces;
using GymApp.Application.Trainers.Commands.AddTrainerSpecialization;
using GymApp.Application.Trainers.Commands.CreateTrainer;
using GymApp.Application.Trainers.Queries.DTOs;
using GymApp.Domain.Constants;
using GymApp.Domain.Exceptions;
using GymApp.Domain.Trainers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using ValidationException = GymApp.Domain.Exceptions.ValidationException;

namespace GymApp.Application.Trainers;

public class TrainerService : ITrainerService
{
    private readonly IAppDbContext _dbContext;
    private readonly IIdentityService _identityService;
    private readonly IValidator<CreateTrainerCommand> _createTrainerValidator;
    private readonly IValidator<AddTrainerSpecializationCommand> _addTrainerSpecializationValidator;
    
    public TrainerService(
        IAppDbContext dbContext, 
        IIdentityService identityService,
        IValidator<CreateTrainerCommand> createTrainerValidator,
        IValidator<AddTrainerSpecializationCommand> addTrainerSpecializationValidator)
    {
        _dbContext = dbContext;
        _identityService = identityService;
        _createTrainerValidator = createTrainerValidator;
        _addTrainerSpecializationValidator = addTrainerSpecializationValidator;
    }
    
    public async Task<Guid> CreateTrainerAsync(
        CreateTrainerCommand command, 
        CancellationToken ct = default)
    {
        // юзаємо валідацію
        await _createTrainerValidator
            .ValidateAndThrowAsync(command, ct);
        
        // перевірка на зайнятість email
        var emailExists = await _identityService
            .EmailExistsAsync(command.Email, ct);

        if (emailExists)
        {
            throw new DomainException($"Користувач з такою поштою ʼ{command.Email}ʼ вже існує.");
        }
        
        // старт транзакції
        await using var transanction = await _dbContext.BeginTransactionAsync(ct);

        try
        {
            // створення юзера разом з роллю тренера
            var (userId, errors) = await _identityService.CreateUserAsync(
                command.Email,
                command.Password,
                command.FirstName,
                command.LastName,
                command.DateOfBirth,
                AppRoles.Trainer,
                ct
            );

            if (errors.Count > 0)
            {
                throw new ValidationException(errors);
            }
            
            // якщо направлення були вибрані (передані)
            if (command.SpecializationIds is { Count: > 0 })
            {
                var countInDb = await _dbContext.Specializations
                    .CountAsync(s => command.SpecializationIds.Contains(s.Id), ct);

                if (countInDb != command.SpecializationIds.Count)
                {
                    throw new DomainException("Одна або кілька специалізацій не знайдені в базі даних.");
                }
            }

            // створюємо профіль тренера
            var profile = TrainerProfile.Create(userId, command.Bio, command.ExperienceYears);

            if (command.SpecializationIds is { Count: > 0 })
            {
                foreach (var specId in command.SpecializationIds)
                {
                    profile.AddSpecialization(specId);
                }
            }

            // зберігаємо в БД
            _dbContext.TrainerProfiles.Add(profile);
            await _dbContext.SaveChangesAsync(ct);

            // коміт транзакції
            await transanction.CommitAsync(ct);

            return profile.Id;
        }
        catch
        {
            // відкат якщо проблемка сталася
            await transanction.RollbackAsync(ct);
            throw;
        }
    }

    public async Task AddSpecializationAsync(AddTrainerSpecializationCommand command, CancellationToken ct = default)
    {
        await _addTrainerSpecializationValidator.ValidateAndThrowAsync(command, ct);

        var profile = await _dbContext.TrainerProfiles
                          .Include(x => x.Specializations)
                          .FirstOrDefaultAsync(x => x.Id == command.TrainerProfileId, ct)
                      ?? throw new NotFoundException(nameof(TrainerProfile), command.TrainerProfileId);

        var specExists = await _dbContext.Specializations
            .AnyAsync(x => x.Id == command.SpecializationId, ct);

        if (!specExists)
        {
            throw new NotFoundException(nameof(Specialization), command.SpecializationId);
        }

        profile.AddSpecialization(command.SpecializationId);
        await _dbContext.SaveChangesAsync(ct);
    }

    public async Task<TrainerDto> GetByIdAsync(
        Guid trainerProfileId, 
        CancellationToken ct = default)
    {
        var profile = await _dbContext.TrainerProfiles
            .AsNoTracking()
            .Include(p => p.Specializations)
            .ThenInclude(s => s.Specialization)
            .FirstOrDefaultAsync(x => x.Id == trainerProfileId, ct)
            ?? throw new NotFoundException(nameof(TrainerProfile), trainerProfileId);

        var user = await _identityService.GetUserByIdAsync(profile.UserId, ct)
            ?? throw new NotFoundException("User", profile.UserId);
        
        return MapToDto(profile, user);
    }

    public async Task<IReadOnlyCollection<TrainerDto>> GetAllAsync(CancellationToken ct = default)
    {
        var profiles = await _dbContext.TrainerProfiles
            .AsNoTracking()
            .Include(p => p.Specializations)
            .ThenInclude(s => s.Specialization)
            .ToListAsync(ct);
        
        var userIds = profiles.Select(p => p.UserId).ToList();
        
        var users = await _identityService.GetUsersByIdsAsync(userIds, ct);
        
        return profiles
            .Where(p => users.ContainsKey(p.UserId))
            .Select(p => MapToDto(p, users[p.UserId]))
            .ToList();
    }
    
    private static TrainerDto MapToDto(TrainerProfile profile, UserDto user) => new(
        profile.Id,
        user.Id,
        user.FirstName,
        user.LastName,
        user.Email,
        profile.Bio,
        profile.ExperienceYears,
        profile.IsAvailable,
        profile.Specializations.Select(s => new SpecializationDto(
            s.Specialization.Id,
            s.Specialization.Name,
            s.Specialization.Description
        )).ToList()
    );
}
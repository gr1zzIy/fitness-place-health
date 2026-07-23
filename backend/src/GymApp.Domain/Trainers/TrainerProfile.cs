using GymApp.Domain.Exceptions;
using GymApp.Domain.Schedule;

namespace GymApp.Domain.Trainers;

public sealed class TrainerProfile
{
    private readonly List<TrainerSpecialization> _specializations = new();
    private readonly List<TrainingSession> _trainingSessions = new();
    
    private TrainerProfile() { }
    
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public string? Bio { get; private set; }
    public int ExperienceYears { get; private set; }

    public bool IsAvailable { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    
    public IReadOnlyCollection<TrainerSpecialization> Specializations => _specializations;
    public IReadOnlyCollection<TrainingSession> TrainingSessions => _trainingSessions;
    
    /// <summary>
    /// Створення тренера
    /// </summary>
    /// <param name="userId">ID користувача</param>
    /// <param name="bio">Біографія</param>
    /// <param name="experienceYears">Роки досвіду</param>
    /// <returns>Профіль тренера</returns>
    /// <exception cref="DomainException"></exception>
    public static TrainerProfile Create(Guid userId, string? bio, int experienceYears)
    {
        if (userId == Guid.Empty)
            throw new DomainException("UserId не може бути порожнім.");
        
        if (experienceYears < 0)
            throw new DomainException("Досвід роботи не може бути від'ємним.");
        
        return new TrainerProfile
        {
            Id = Guid.NewGuid(),
            UserId = userId,
            Bio = bio?.Trim(),
            ExperienceYears = experienceYears,
            IsAvailable = true,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }
    
    /// <summary>
    /// Додавання спеціалізації
    /// </summary>
    /// <param name="specializationId"></param>
    /// <exception cref="DomainException"></exception>
    public void AddSpecialization(Guid specializationId)
    {
        if (specializationId == Guid.Empty)
            throw new DomainException("SpecializationId не може бути порожнім.");

        if (!_specializations.Any(s => s.SpecializationId == specializationId))
        {
            _specializations.Add(new TrainerSpecialization(Id, specializationId));
        }
    }
}
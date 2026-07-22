using GymApp.Domain.Schedule;

namespace GymApp.Domain.Trainers;

public sealed class TrainerProfile
{
    private readonly List<TrainerSpecialization> _specializations = new();
    private readonly List<TrainingSession> _trainingSessions = new();
    
    public Guid Id { get; private set; }
    public Guid UserId { get; private set; }

    public string? Bio { get; private set; }
    public int ExperienceYears { get; private set; }

    public bool IsAvailable { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    
    public IReadOnlyCollection<TrainerSpecialization> Specializations => _specializations;
    public IReadOnlyCollection<TrainingSession> TrainingSessions => _trainingSessions;
}
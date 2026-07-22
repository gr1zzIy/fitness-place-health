using GymApp.Domain.Gyms;

namespace GymApp.Domain.Schedule;

public sealed class TrainingType
{
    private readonly List<TrainingSession> _trainingSessions = new();
    
    public Guid Id { get; private set; }
    public Guid GymId { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public int DefaultDurationMinutes { get; private set; }
    public int? DefaultCapacity { get; private set; }

    public TrainingFormat Format { get; private set; }

    public bool IsActive { get; private set; }
    
    public Gym Gym { get; private set; } = null!;
    public IReadOnlyCollection<TrainingSession> TrainingSessions => _trainingSessions;
}
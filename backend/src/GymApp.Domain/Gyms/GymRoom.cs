using GymApp.Domain.Schedule;

namespace GymApp.Domain.Gyms;

public sealed class GymRoom
{
    private readonly List<TrainingSession> _trainingSessions = new();
    
    public Guid Id { get; private set; }
    public Guid GymBranchId { get; private set; }

    public string Name { get; private set; } = null!;
    public int Capacity { get; private set; }

    public bool IsActive { get; private set; }
    
    public GymBranch GymBranch { get; private set; } = null!;
    public IReadOnlyCollection<TrainingSession> TrainingSessions => _trainingSessions;
}
using GymApp.Domain.Memberships;
using GymApp.Domain.Schedule;

namespace GymApp.Domain.Gyms;

public sealed class Gym
{
    private readonly List<GymBranch> _branches = new();
    private readonly List<TrainingType> _trainingTypes = new();
    private readonly List<MembershipPlan> _membershipPlans = new();
    
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public string TimeZone { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }
    
    public IReadOnlyCollection<GymBranch> Branches => _branches;
    public IReadOnlyCollection<TrainingType> TrainingTypes => _trainingTypes;
    public IReadOnlyCollection<MembershipPlan> MembershipPlans => _membershipPlans;
}
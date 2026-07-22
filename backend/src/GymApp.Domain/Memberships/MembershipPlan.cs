using GymApp.Domain.Gyms;

namespace GymApp.Domain.Memberships;

public sealed class MembershipPlan
{
    private readonly List<ClientMembership> _clientMemberships = new();
    
    public Guid Id { get; private set; }
    public Guid GymId { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public decimal Price { get; private set; }
    public string Currency { get; private set; } = null!;

    public int DurationDays { get; private set; }
    public int? VisitLimit { get; private set; }

    public bool IsUnlimited { get; private set; }
    public bool IsActive { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    
    public Gym Gym { get; private set; } = null!;
    public IReadOnlyCollection<ClientMembership> ClientMemberships => _clientMemberships;
}
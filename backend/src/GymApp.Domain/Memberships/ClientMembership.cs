namespace GymApp.Domain.Memberships;

public sealed class ClientMembership
{
    public Guid Id { get; private set; }

    public Guid ClientUserId { get; private set; }
    public Guid MembershipPlanId { get; private set; }

    public DateTimeOffset StartsAt { get; private set; }
    public DateTimeOffset ExpiresAt { get; private set; }

    public int? VisitLimit { get; private set; }
    public int VisitsUsed { get; private set; }

    public decimal PurchasedPrice { get; private set; }
    public string Currency { get; private set; } = null!;
    
    public MembershipStatus Status { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
}
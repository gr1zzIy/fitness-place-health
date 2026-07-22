namespace GymApp.Domain.Gyms;

public sealed class GymBranch
{
    private readonly List<GymRoom> _rooms = new();
    
    public Guid Id { get; private set; }
    public Guid GymId { get; private set; }

    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }

    public bool IsActive { get; private set; }
    
    public Gym Gym { get; private set; } = null!;
    public IReadOnlyCollection<GymRoom> Rooms => _rooms;
}
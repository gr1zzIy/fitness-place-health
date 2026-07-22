namespace GymApp.Domain.Trainers;

public sealed class Specialization
{
    private readonly List<TrainerSpecialization> _trainers = new();
    
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public bool IsActive { get; private set; }
    
    public IReadOnlyCollection<TrainerSpecialization> Trainers => _trainers;
}
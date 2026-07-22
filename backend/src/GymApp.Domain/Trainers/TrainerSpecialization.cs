namespace GymApp.Domain.Trainers;

public sealed class TrainerSpecialization
{
    public Guid TrainerProfileId { get; private set; }
    public Guid SpecializationId { get; private set; }
    
    public TrainerProfile TrainerProfile { get; private set; } = null!;
    public Specialization Specialization { get; private set; } = null!;
}
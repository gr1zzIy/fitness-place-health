using GymApp.Domain.Exceptions;
using GymApp.Domain.Schedule;

namespace GymApp.Domain.Gyms;

public sealed class GymRoom
{
    private readonly List<TrainingSession> _trainingSessions = new();
    
    private GymRoom() { }
    
    public Guid Id { get; private set; }
    public Guid GymBranchId { get; private set; }

    public string Name { get; private set; } = null!;
    public int Capacity { get; private set; }

    public bool IsActive { get; private set; }
    
    public GymBranch GymBranch { get; private set; } = null!;
    public IReadOnlyCollection<TrainingSession> TrainingSessions => _trainingSessions;
    
    /// <summary>
    /// Створення кімнати у філії
    /// </summary>
    /// <param name="gymBranchId">ІД філії</param>
    /// <param name="name">Назва зали</param>
    /// <param name="capacity">Місткість зали</param>
    /// <returns>Створює обʼєкт</returns>
    /// <exception cref="DomainException">Виникає, якщо вхідні дані є недійсними</exception>
    public static GymRoom Create(Guid gymBranchId, string name, int capacity)
    {
        if (gymBranchId == Guid.Empty)
            throw new DomainException("GymBranchId не може бути порожнім.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Назва зали є обов'язковою.");

        if (capacity <= 0)
            throw new DomainException("Місткість зали повинна бути більше 0.");

        return new GymRoom
        {
            Id = Guid.NewGuid(),
            GymBranchId = gymBranchId,
            Name = name.Trim(),
            Capacity = capacity,
            IsActive = true
        };
    }
}
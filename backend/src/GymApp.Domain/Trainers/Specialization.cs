using GymApp.Domain.Exceptions;

namespace GymApp.Domain.Trainers;

public sealed class Specialization
{
    private readonly List<TrainerSpecialization> _trainers = new();
    
    private Specialization() { }
    
    public Guid Id { get; private set; }

    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }

    public bool IsActive { get; private set; }
    
    public IReadOnlyCollection<TrainerSpecialization> Trainers => _trainers;
    
    /// <summary>
    /// Створення спеціалізації
    /// </summary>
    /// <param name="name">Назва спеціалізації</param>
    /// <param name="description">Опис спеціалізації</param>
    /// <returns>Об'єкт спеціалізації</returns>
    /// <exception cref="DomainException">Виникає, якщо назва спеціалізації порожня або null</exception>
    public static Specialization Create(string name, string? description)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Назва спеціалізації обов'язкова.");

        return new Specialization
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim(),
            IsActive = true
        };
    }
}
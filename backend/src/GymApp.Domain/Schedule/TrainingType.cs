using GymApp.Domain.Exceptions;
using GymApp.Domain.Gyms;

namespace GymApp.Domain.Schedule;

public sealed class TrainingType
{
    private readonly List<TrainingSession> _trainingSessions = new();
    
    private TrainingType() { }
    
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
    
    /// <summary>
    /// Створення типу тренувань
    /// </summary>
    /// <param name="gymId">Ідентифікатор спортклубу</param>
    /// <param name="name">Назва типу тренування</param>
    /// <param name="description">Опис типу тренування</param>
    /// <param name="defaultDurationMinutes">Тривалість за замовчуванням (хвилини)</param>
    /// <param name="defaultCapacity">Місткість за замовчуванням</param>
    /// <param name="format">Формат тренування</param>
    /// <returns>Створює обʼєкт</returns>
    /// <exception cref="DomainException">Виникає, якщо передано некоректні дані</exception>
    public static TrainingType Create(
        Guid gymId,
        string name,
        string? description,
        int defaultDurationMinutes,
        int? defaultCapacity,
        TrainingFormat format)
    {
        // Доменні інваріанти
        if (gymId == Guid.Empty)
            throw new DomainException("GymId не може бути порожнім.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Назва типу тренування є обов'язковою.");

        if (defaultDurationMinutes <= 0)
            throw new DomainException("Зауважена тривалість тренування повинна бути більше 0 хвилин.");

        if (defaultCapacity.HasValue && defaultCapacity.Value <= 0)
            throw new DomainException("Зауважена місткість тренування повинна бути більше 0.");

        return new TrainingType
        {
            Id = Guid.NewGuid(),
            GymId = gymId,
            Name = name.Trim(),
            Description = description?.Trim(),
            DefaultDurationMinutes = defaultDurationMinutes,
            DefaultCapacity = defaultCapacity,
            Format = format,
            IsActive = true
        };
    }

    /// <summary>
    /// Оновлення типу тренувань
    /// </summary>
    /// <param name="name">Назва типу тренування</param>
    /// <param name="description">Опис типу тренування</param>
    /// <param name="defaultDurationMinutes">Тривалість за замовчуванням (хвилини)</param>
    /// <param name="defaultCapacity">Місткість за замовчуванням</param>
    /// <param name="format">Формат тренування</param>
    /// <param name="isActive">Статус активності</param>
    /// <exception cref="DomainException">Виникає, якщо передано некоректні дані</exception>
    public void Update(
        string name,
        string? description,
        int defaultDurationMinutes,
        int? defaultCapacity,
        TrainingFormat format,
        bool isActive)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Назва типу тренування є обов'язковою.");

        if (defaultDurationMinutes <= 0)
            throw new DomainException("Зауважена тривалість тренування повинна бути більше 0 хвилин.");

        if (defaultCapacity.HasValue && defaultCapacity.Value <= 0)
            throw new DomainException("Зауважена місткість тренування повинна бути більше 0.");

        Name = name.Trim();
        Description = description?.Trim();
        DefaultDurationMinutes = defaultDurationMinutes;
        DefaultCapacity = defaultCapacity;
        Format = format;
        IsActive = isActive;
    }
}
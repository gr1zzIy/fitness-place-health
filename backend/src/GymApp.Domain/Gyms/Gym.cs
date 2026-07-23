using GymApp.Domain.Exceptions;
using GymApp.Domain.Memberships;
using GymApp.Domain.Schedule;

namespace GymApp.Domain.Gyms;

public sealed class Gym
{
    private readonly List<GymBranch> _branches = new();
    private readonly List<TrainingType> _trainingTypes = new();
    private readonly List<MembershipPlan> _membershipPlans = new();
    
    private Gym() { }
    
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
    
    /// <summary>
    /// Створення спортзалу
    /// </summary>
    /// <param name="name">Назва</param>
    /// <param name="description">Опис</param>
    /// <param name="timeZone">Часовий пояс</param>
    /// <returns>Створює обʼєкт</returns>
    /// <exception cref="DomainException">Виняток, що виникає при некоректних даних</exception>
    public static Gym Create(string name, string? description, string timeZone)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Назва спортзалу не може бути порожньою.");

        if (string.IsNullOrWhiteSpace(timeZone))
            throw new DomainException("Часовий пояс є обов'язковим.");

        return new Gym
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim(),
            TimeZone = timeZone.Trim(),
            IsActive = true,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    /// <summary>
    /// Оновлення інформації про спортзал
    /// </summary>
    /// <param name="name">Назва</param>
    /// <param name="description">Опис</param>
    /// <param name="timeZone">Часовий пояс</param>
    /// <param name="isActive">Статус активності</param>
    /// <exception cref="DomainException">Виняток, що виникає при некоректних даних</exception>
    public void Update(string name, string? description, string timeZone, bool isActive)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Назва спортзалу не може бути порожньою.");

        if (string.IsNullOrWhiteSpace(timeZone))
            throw new DomainException("Часовий пояс є обов'язковим.");

        Name = name.Trim();
        Description = description?.Trim();
        TimeZone = timeZone.Trim();
        IsActive = isActive;
        UpdatedAt = DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Додавання філії до спортзалу
    /// </summary>
    /// <param name="name">Назва</param>
    /// <param name="address">Адреса</param>
    /// <param name="phoneNumber">Номер телефону</param>
    /// <returns>Створює обʼєкт філії</returns>
    /// <exception cref="DomainException">Виняток, що виникає при некоректних даних</exception>
    public GymBranch AddBranch(string name, string address, string? phoneNumber)
    {
        var branch = GymBranch.Create(Id, name, address, phoneNumber);
        _branches.Add(branch);
        return branch;
    }
}
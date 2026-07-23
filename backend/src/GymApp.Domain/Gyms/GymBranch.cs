using GymApp.Domain.Exceptions;

namespace GymApp.Domain.Gyms;

public sealed class GymBranch
{
    private readonly List<GymRoom> _rooms = new();
    
    private GymBranch() { }
    
    public Guid Id { get; private set; }
    public Guid GymId { get; private set; }

    public string Name { get; private set; } = null!;
    public string Address { get; private set; } = null!;
    public string? PhoneNumber { get; private set; }

    public bool IsActive { get; private set; }
    
    public Gym Gym { get; private set; } = null!;
    public IReadOnlyCollection<GymRoom> Rooms => _rooms;
    
    /// <summary>
    /// Створення філії
    /// </summary>
    /// <param name="gymId">ІД спортзалу</param>
    /// <param name="name">Назва філії</param>
    /// <param name="address">Адреса філії</param>
    /// <param name="phoneNumber">Номер телефону</param>
    /// <returns>Створює обʼєкт</returns>
    /// <exception cref="DomainException">Виникає, якщо вхідні дані є недійсними</exception>
    public static GymBranch Create(Guid gymId, string name, string address, string? phoneNumber)
    {
        if (gymId == Guid.Empty)
            throw new DomainException("GymId не може бути порожнім.");

        if (string.IsNullOrWhiteSpace(name))
            throw new DomainException("Назва філії є обов'язковою.");

        if (string.IsNullOrWhiteSpace(address))
            throw new DomainException("Адреса філії є обов'язковою.");

        return new GymBranch
        {
            Id = Guid.NewGuid(),
            GymId = gymId,
            Name = name.Trim(),
            Address = address.Trim(),
            PhoneNumber = phoneNumber?.Trim(),
            IsActive = true
        };
    }

    /// <summary>
    /// Додає кімнату до філії
    /// </summary>
    /// <param name="name">Назва кімнати</param>
    /// <param name="capacity">Місткість кімнати</param>
    /// <returns>Створює обʼєкт кімнати</returns>
    public GymRoom AddRoom(string name, int capacity)
    {
        var room = GymRoom.Create(Id, name, capacity);
        _rooms.Add(room);
        return room;
    }
}
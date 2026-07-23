using GymApp.Domain.Attendances;
using GymApp.Domain.Bookings;
using GymApp.Domain.Exceptions;
using GymApp.Domain.Gyms;
using GymApp.Domain.Trainers;

namespace GymApp.Domain.Schedule;

public sealed class TrainingSession
{
    private readonly List<Booking> _bookings = new();
    private readonly List<Attendance> _attendances = new();
    
    private TrainingSession() { }
    
    public Guid Id { get; private set; }

    public Guid GymBranchId { get; private set; }
    public Guid GymRoomId { get; private set; }
    public Guid TrainingTypeId { get; private set; }
    public Guid TrainerProfileId { get; private set; }

    public DateTimeOffset StartsAt { get; private set; }
    public DateTimeOffset EndsAt { get; private set; }

    public int Capacity { get; private set; }

    public TrainingSessionStatus Status { get; private set; }

    public string? CancellationReason { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
    public DateTimeOffset? UpdatedAt { get; private set; }

    public GymRoom GymRoom { get; private set; } = null!;
    public TrainingType TrainingType { get; private set; } = null!;
    public TrainerProfile TrainerProfile { get; private set; } = null!;
    public IReadOnlyCollection<Booking> Bookings => _bookings;
    public IReadOnlyCollection<Attendance> Attendances => _attendances;
    
    /// <summary>
    /// Створення заняття
    /// </summary>
    /// <param name="gymBranchId">Філія залу</param>
    /// <param name="gymRoomId">Кімната залу</param>
    /// <param name="trainingTypeId">Тип тренування</param>
    /// <param name="trainerProfileId">Профіль тренера</param>
    /// <param name="startsAt">Час початку</param>
    /// <param name="endsAt">Час завершення</param>
    /// <param name="capacity">Місткість</param>
    /// <returns>Готовий обʼєкт</returns>
    /// <exception cref="DomainException">Помилка</exception>
    public static TrainingSession Create(
        Guid gymBranchId,
        Guid gymRoomId,
        Guid trainingTypeId,
        Guid trainerProfileId,
        DateTimeOffset startsAt,
        DateTimeOffset endsAt,
        int capacity)
    {
        // Валідація ідентифікаторів
        if (gymBranchId == Guid.Empty || gymRoomId == Guid.Empty || 
            trainingTypeId == Guid.Empty || trainerProfileId == Guid.Empty)
            throw new DomainException("Ідентифікатори сутностей не можуть бути порожніми.");

        // Валідація часу
        if (startsAt >= endsAt)
            throw new DomainException("Час завершення тренування повинен бути пізніше часу початку.");

        // Валідація місткості
        if (capacity <= 0)
            throw new DomainException("Місткість тренування повинна бути більше 0.");
        
        return new TrainingSession
        {
            Id = Guid.NewGuid(),
            GymBranchId = gymBranchId,
            GymRoomId = gymRoomId,
            TrainingTypeId = trainingTypeId,
            TrainerProfileId = trainerProfileId,
            StartsAt = startsAt,
            EndsAt = endsAt,
            Capacity = capacity,
            Status = TrainingSessionStatus.Scheduled,
            CreatedAt = DateTimeOffset.UtcNow
        };
    }

    /// <summary>
    /// Скасування заняття
    /// </summary>
    /// <param name="reason">Причина скасування</param>
    /// <exception cref="DomainException">Помилка</exception>
    public void Cancel(string reason)
    {
        if (Status == TrainingSessionStatus.Cancelled)
            throw new DomainException("Сесію вже скасовано.");

        if (Status == TrainingSessionStatus.Completed)
            throw new DomainException("Неможливо скасувати вже завершене тренування.");
        
        if (string.IsNullOrWhiteSpace(reason))
            throw new DomainException("Причина скасування не може бути порожньою.");

        Status = TrainingSessionStatus.Cancelled;
        CancellationReason = reason;
        UpdatedAt = DateTimeOffset.UtcNow;
    }
}
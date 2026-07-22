using GymApp.Domain.Attendances;
using GymApp.Domain.Bookings;
using GymApp.Domain.Gyms;
using GymApp.Domain.Trainers;

namespace GymApp.Domain.Schedule;

public sealed class TrainingSession
{
    private readonly List<Booking> _bookings = new();
    private readonly List<Attendance> _attendances = new();
    
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
}
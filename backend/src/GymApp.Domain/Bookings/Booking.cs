using GymApp.Domain.Attendances;

namespace GymApp.Domain.Bookings;

public sealed class Booking
{
    public Guid Id { get; private set; }

    public Guid TrainingSessionId { get; private set; }
    public Guid ClientUserId { get; private set; }

    public BookingStatus Status { get; private set; }

    public DateTimeOffset BookedAt { get; private set; }
    public DateTimeOffset? CancelledAt { get; private set; }

    public string? CancellationReason { get; private set; }
    
    public Attendance Attendance { get; private set; } = null!;
}
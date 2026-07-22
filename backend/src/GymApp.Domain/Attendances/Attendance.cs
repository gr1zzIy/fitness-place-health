namespace GymApp.Domain.Attendances;

public sealed class Attendance
{
    public Guid Id { get; private set; }

    public Guid ClientUserId { get; private set; }
    public Guid TrainingSessionId { get; private set; }
    public Guid? BookingId { get; private set; }

    public AttendanceStatus Status { get; private set; }

    public DateTimeOffset? CheckedInAt { get; private set; }
    public DateTimeOffset? CheckedOutAt { get; private set; }

    public Guid? MarkedByUserId { get; private set; }

    public DateTimeOffset CreatedAt { get; private set; }
}
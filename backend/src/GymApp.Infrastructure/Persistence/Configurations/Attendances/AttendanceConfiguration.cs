using GymApp.Domain.Attendances;
using GymApp.Domain.Bookings;
using GymApp.Domain.Schedule;
using GymApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Attendances;

public sealed class AttendanceConfiguration
    : IEntityTypeConfiguration<Attendance>
{
    public void Configure(
        EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("attendances");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .HasOne<TrainingSession>()
            .WithMany(x => x.Attendances)
            .HasForeignKey(x => x.TrainingSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.ClientUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<Booking>()
            .WithOne(x => x.Attendance)
            .HasForeignKey<Attendance>(
                x => x.BookingId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.MarkedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.TrainingSessionId);
        builder.HasIndex(x => x.ClientUserId);

        builder.HasIndex(x => x.BookingId)
            .IsUnique();

        builder.HasIndex(x => new
            {
                x.TrainingSessionId,
                x.ClientUserId
            })
            .IsUnique();
    }
}
using GymApp.Domain.Bookings;
using GymApp.Domain.Schedule;
using GymApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Bookings;

public sealed class BookingConfiguration
    : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.BookedAt)
            .IsRequired();

        builder.Property(x => x.CancelledAt);

        builder
            .Property(x => x.CancellationReason)
            .HasMaxLength(1000);

        builder
            .HasOne<TrainingSession>()
            .WithMany(x => x.Bookings)
            .HasForeignKey(x => x.TrainingSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.ClientUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.TrainingSessionId);
        builder.HasIndex(x => x.ClientUserId);
        builder.HasIndex(x => x.Status);

        builder.HasIndex(x => new
            {
                x.TrainingSessionId,
                x.ClientUserId
            })
            .IsUnique()
            .HasFilter("status IN (1, 3)");
    }
}
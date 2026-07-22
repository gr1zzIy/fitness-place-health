using GymApp.Domain.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Schedule;

public sealed class TrainingSessionConfiguration
    : IEntityTypeConfiguration<TrainingSession>
{
    public void Configure(EntityTypeBuilder<TrainingSession> builder)
    {
        builder.ToTable("training_sessions");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.StartsAt)
            .IsRequired();

        builder
            .Property(x => x.EndsAt)
            .IsRequired();

        builder
            .Property(x => x.Capacity)
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.CancellationReason)
            .HasMaxLength(1000);

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .HasOne(x => x.GymRoom)
            .WithMany(x => x.TrainingSessions)
            .HasForeignKey(x => x.GymRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.TrainingType)
            .WithMany(x => x.TrainingSessions)
            .HasForeignKey(x => x.TrainingTypeId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne(x => x.TrainerProfile)
            .WithMany(x => x.TrainingSessions)
            .HasForeignKey(x => x.TrainerProfileId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
        {
            x.GymRoomId,
            x.StartsAt
        });

        builder.HasIndex(x => new
        {
            x.TrainerProfileId,
            x.StartsAt
        });

        builder.HasIndex(x => new
        {
            x.TrainingTypeId,
            x.StartsAt
        });

        builder.HasIndex(x => x.Status);

        builder.ToTable(tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "ck_training_sessions_valid_period",
                "ends_at > starts_at");

            tableBuilder.HasCheckConstraint(
                "ck_training_sessions_capacity_positive",
                "capacity > 0");
        });
    }
}
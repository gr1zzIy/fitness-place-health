using GymApp.Domain.Schedule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Schedule;

public sealed class TrainingTypeConfiguration
    : IEntityTypeConfiguration<TrainingType>
{
    public void Configure(EntityTypeBuilder<TrainingType> builder)
    {
        builder.ToTable("training_types");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(2000);

        builder
            .Property(x => x.DefaultDurationMinutes)
            .IsRequired();

        builder.Property(x => x.DefaultCapacity);

        builder
            .Property(x => x.Format)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .IsRequired();

        builder
            .HasOne(x => x.Gym)
            .WithMany(x => x.TrainingTypes)
            .HasForeignKey(x => x.GymId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => new
            {
                x.GymId,
                x.Name
            })
            .IsUnique();

        builder.ToTable(tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "ck_training_types_duration_positive",
                "default_duration_minutes > 0");

            tableBuilder.HasCheckConstraint(
                "ck_training_types_capacity_positive",
                "default_capacity IS NULL OR default_capacity > 0");
        });
    }
}
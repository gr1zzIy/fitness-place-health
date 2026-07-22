using GymApp.Domain.Trainers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Trainers;

public sealed class TrainerSpecializationConfiguration
    : IEntityTypeConfiguration<TrainerSpecialization>
{
    public void Configure(
        EntityTypeBuilder<TrainerSpecialization> builder)
    {
        builder.ToTable("trainer_specializations");

        builder.HasKey(x => new
        {
            x.TrainerProfileId,
            x.SpecializationId
        });

        builder.HasOne(x => x.TrainerProfile)
            .WithMany(x => x.Specializations)
            .HasForeignKey(x => x.TrainerProfileId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(x => x.Specialization)
            .WithMany(x => x.Trainers)
            .HasForeignKey(x => x.SpecializationId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
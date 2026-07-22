using GymApp.Domain.Trainers;
using GymApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Trainers;

public sealed class TrainerProfileConfiguration : IEntityTypeConfiguration<TrainerProfile>
{
    public void Configure(EntityTypeBuilder<TrainerProfile> builder)
    {
        builder.ToTable("trainer_profiles");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Bio)
            .HasMaxLength(3000);

        builder
            .Property(x => x.ExperienceYears)
            .IsRequired();

        builder
            .Property(x => x.IsAvailable)
            .IsRequired();

        builder
            .HasOne<ApplicationUser>()
            .WithOne()
            .HasForeignKey<TrainerProfile>(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(x => x.UserId)
            .IsUnique();

        builder.ToTable(tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "ck_trainer_profiles_experience_years",
                "experience_years >= 0");
        });
    }
}
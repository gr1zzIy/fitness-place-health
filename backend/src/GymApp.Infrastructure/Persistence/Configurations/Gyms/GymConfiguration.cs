using GymApp.Domain.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Gyms;

public sealed class GymConfiguration : IEntityTypeConfiguration<Gym>
{
    public void Configure(EntityTypeBuilder<Gym> builder)
    {
        builder.ToTable("gyms");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(2000);

        builder
            .Property(x => x.TimeZone)
            .HasMaxLength(100)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder.HasIndex(x => x.Name);

        builder
            .HasMany(x => x.Branches)
            .WithOne(x => x.Gym)
            .HasForeignKey(x => x.GymId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.TrainingTypes)
            .WithOne(x => x.Gym)
            .HasForeignKey(x => x.GymId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasMany(x => x.MembershipPlans)
            .WithOne(x => x.Gym)
            .HasForeignKey(x => x.GymId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
using GymApp.Domain.Trainers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Trainers;

public sealed class SpecializationConfiguration
    : IEntityTypeConfiguration<Specialization>
{
    public void Configure(EntityTypeBuilder<Specialization> builder)
    {
        builder.ToTable("specializations");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(1000);

        builder
            .Property(x => x.IsActive)
            .IsRequired();

        builder
            .HasIndex(x => x.Name)
            .IsUnique();
    }
}
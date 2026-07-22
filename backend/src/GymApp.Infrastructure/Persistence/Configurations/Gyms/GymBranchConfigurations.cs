using GymApp.Domain.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Gyms;

public sealed class GymBranchConfiguration : IEntityTypeConfiguration<GymBranch>
{
    public void Configure(EntityTypeBuilder<GymBranch> builder)
    {
        builder.ToTable("gym_branches");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(x => x.Address)
            .HasMaxLength(500)
            .IsRequired();

        builder
            .Property(x => x.PhoneNumber)
            .HasMaxLength(30);

        builder
            .Property(x => x.IsActive)
            .IsRequired();

        builder
            .HasOne(x => x.Gym)
            .WithMany(x => x.Branches)
            .HasForeignKey(x => x.GymId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(x => new
            {
                x.GymId,
                x.Name
            })
            .IsUnique();
    }
}
using GymApp.Domain.Gyms;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Gyms;

public sealed class GymRoomConfiguration
    : IEntityTypeConfiguration<GymRoom>
{
    public void Configure(EntityTypeBuilder<GymRoom> builder)
    {
        builder.ToTable("gym_rooms");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(150)
            .IsRequired();

        builder
            .Property(x => x.Capacity)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .IsRequired();

        builder
            .HasOne(x => x.GymBranch)
            .WithMany(x => x.Rooms)
            .HasForeignKey(x => x.GymBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasIndex(x => new
            {
                x.GymBranchId,
                x.Name
            })
            .IsUnique();

        builder.ToTable(tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "ck_gym_rooms_capacity_positive",
                "capacity > 0");
        });
    }
}
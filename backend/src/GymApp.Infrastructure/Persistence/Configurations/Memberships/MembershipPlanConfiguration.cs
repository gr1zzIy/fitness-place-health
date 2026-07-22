using GymApp.Domain.Memberships;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Memberships;

public sealed class MembershipPlanConfiguration
    : IEntityTypeConfiguration<MembershipPlan>
{
    public void Configure(EntityTypeBuilder<MembershipPlan> builder)
    {
        builder.ToTable("membership_plans");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.Name)
            .HasMaxLength(200)
            .IsRequired();

        builder
            .Property(x => x.Description)
            .HasMaxLength(2000);

        builder
            .Property(x => x.Price)
            .HasPrecision(12, 2)
            .IsRequired();

        builder
            .Property(x => x.Currency)
            .HasMaxLength(3)
            .IsFixedLength()
            .IsRequired();

        builder
            .Property(x => x.DurationDays)
            .IsRequired();

        builder.Property(x => x.VisitLimit);

        builder
            .Property(x => x.IsUnlimited)
            .IsRequired();

        builder
            .Property(x => x.IsActive)
            .IsRequired();

        builder
            .HasOne(x => x.Gym)
            .WithMany(x => x.MembershipPlans)
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
                "ck_membership_plans_price_non_negative",
                "price >= 0");

            tableBuilder.HasCheckConstraint(
                "ck_membership_plans_duration_positive",
                "duration_days > 0");

            tableBuilder.HasCheckConstraint(
                "ck_membership_plans_visit_limit_positive",
                "visit_limit IS NULL OR visit_limit > 0");
        });
    }
}
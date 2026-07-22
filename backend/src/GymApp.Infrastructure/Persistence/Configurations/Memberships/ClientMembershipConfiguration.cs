using GymApp.Domain.Memberships;
using GymApp.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GymApp.Infrastructure.Persistence.Configurations.Memberships;

public sealed class ClientMembershipConfiguration
    : IEntityTypeConfiguration<ClientMembership>
{
    public void Configure(EntityTypeBuilder<ClientMembership> builder)
    {
        builder.ToTable("client_memberships");

        builder.HasKey(x => x.Id);

        builder
            .Property(x => x.StartsAt)
            .IsRequired();

        builder
            .Property(x => x.ExpiresAt)
            .IsRequired();

        builder.Property(x => x.VisitLimit);

        builder
            .Property(x => x.VisitsUsed)
            .IsRequired();

        builder
            .Property(x => x.PurchasedPrice)
            .HasPrecision(12, 2)
            .IsRequired();

        builder
            .Property(x => x.Currency)
            .HasMaxLength(3)
            .IsFixedLength()
            .IsRequired();

        builder
            .Property(x => x.Status)
            .HasConversion<int>()
            .IsRequired();

        builder
            .Property(x => x.CreatedAt)
            .IsRequired();

        builder
            .HasOne<MembershipPlan>()
            .WithMany(x => x.ClientMemberships)
            .HasForeignKey(x => x.MembershipPlanId)
            .OnDelete(DeleteBehavior.Restrict);

        builder
            .HasOne<ApplicationUser>()
            .WithMany()
            .HasForeignKey(x => x.ClientUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ClientUserId);

        builder.HasIndex(x => new
        {
            x.ClientUserId,
            x.Status
        });

        builder.HasIndex(x => x.ExpiresAt);

        builder.ToTable(tableBuilder =>
        {
            tableBuilder.HasCheckConstraint(
                "ck_client_memberships_valid_period",
                "expires_at > starts_at");

            tableBuilder.HasCheckConstraint(
                "ck_client_memberships_visits_used_non_negative",
                "visits_used >= 0");

            tableBuilder.HasCheckConstraint(
                "ck_client_memberships_visit_limit_positive",
                "visit_limit IS NULL OR visit_limit > 0");

            tableBuilder.HasCheckConstraint(
                "ck_client_memberships_visits_within_limit",
                "visit_limit IS NULL OR visits_used <= visit_limit");

            tableBuilder.HasCheckConstraint(
                "ck_client_memberships_price_non_negative",
                "purchased_price >= 0");
        });
    }
}
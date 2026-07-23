using GymApp.Application.Common.Interfaces;
using GymApp.Domain.Attendances;
using GymApp.Domain.Bookings;
using GymApp.Domain.Gyms;
using GymApp.Domain.Memberships;
using GymApp.Domain.Schedule;
using GymApp.Domain.Trainers;
using GymApp.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace GymApp.Infrastructure.Persistence;

public class AppDbContext : 
    IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>, 
    IAppDbContext
{
    // Gyms
    public DbSet<Gym> Gyms => Set<Gym>();
    public DbSet<GymBranch> GymBranches => Set<GymBranch>();
    public DbSet<GymRoom> GymRooms => Set<GymRoom>();
    
    // Bookings & Attendances
    public DbSet<Booking> Bookings => Set<Booking>();
    public DbSet<Attendance> Attendances => Set<Attendance>();
    
    // Training & Schedule
    public DbSet<TrainingType> TrainingTypes => Set<TrainingType>();
    public DbSet<TrainingSession> TrainingSessions => Set<TrainingSession>();
    
    // Memberships & Finance
    public DbSet<MembershipPlan> MembershipPlans => Set<MembershipPlan>();
    public DbSet<ClientMembership> ClientMemberships => Set<ClientMembership>();

    // Trainers
    public DbSet<TrainerProfile> TrainerProfiles => Set<TrainerProfile>();
    public DbSet<Specialization> Specializations => Set<Specialization>();
    public DbSet<TrainerSpecialization> TrainerSpecializations => Set<TrainerSpecialization>();
    
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default)
    {
        return Database.BeginTransactionAsync(cancellationToken);
    }    
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }
}
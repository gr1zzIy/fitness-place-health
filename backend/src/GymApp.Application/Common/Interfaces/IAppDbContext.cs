using GymApp.Domain.Attendances;
using GymApp.Domain.Bookings;
using GymApp.Domain.Gyms;
using GymApp.Domain.Memberships;
using GymApp.Domain.Schedule;
using GymApp.Domain.Trainers;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Application.Common.Interfaces;

public interface IAppDbContext
{
    DbSet<Gym> Gyms { get; }
    DbSet<GymBranch> GymBranches { get; }
    DbSet<GymRoom> GymRooms { get; }

    DbSet<TrainerProfile> TrainerProfiles { get; }
    DbSet<Specialization> Specializations { get; }
    DbSet<TrainerSpecialization> TrainerSpecializations { get; }

    DbSet<TrainingType> TrainingTypes { get; }
    DbSet<TrainingSession> TrainingSessions { get; }

    DbSet<Booking> Bookings { get; }
    DbSet<Attendance> Attendances { get; }

    DbSet<MembershipPlan> MembershipPlans { get; }
    DbSet<ClientMembership> ClientMemberships { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
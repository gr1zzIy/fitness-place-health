using GymApp.Application.Gyms.Commands.CreateGym;
using GymApp.Application.Gyms.Commands.CreateGymBranch;
using GymApp.Application.Gyms.Commands.CreateGymRoom;
using GymApp.Application.Gyms.Commands.UpdateGym;
using GymApp.Application.Gyms.Queries.DTOs;

namespace GymApp.Application.Gyms;

public interface IGymService
{
    Task<Guid> CreateGymAsync(CreateGymCommand command, CancellationToken ct = default);
    Task UpdateGymAsync(UpdateGymCommand command, CancellationToken ct = default);
    Task<Guid> CreateBranchAsync(CreateGymBranchCommand command, CancellationToken ct = default);
    Task<Guid> CreateRoomAsync(CreateGymRoomCommand command, CancellationToken ct = default);
    
    Task<GymDto> GetByIdAsync(Guid id, CancellationToken ct = default);
    Task<IReadOnlyCollection<GymDto>> GetAllAsync(CancellationToken ct = default);
    
    Task DeleteGymAsync(Guid id, CancellationToken ct = default);
}
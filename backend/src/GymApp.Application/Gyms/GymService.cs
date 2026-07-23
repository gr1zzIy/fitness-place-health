using FluentValidation;
using GymApp.Application.Common.Extensions;
using GymApp.Application.Common.Interfaces;
using GymApp.Application.Gyms.Commands.CreateGym;
using GymApp.Application.Gyms.Commands.CreateGymBranch;
using GymApp.Application.Gyms.Commands.CreateGymRoom;
using GymApp.Application.Gyms.Commands.UpdateGym;
using GymApp.Application.Gyms.Queries.DTOs;
using GymApp.Domain.Exceptions;
using GymApp.Domain.Gyms;
using Microsoft.EntityFrameworkCore;

namespace GymApp.Application.Gyms;

public sealed class GymService : IGymService
{
    private readonly IAppDbContext _context;
    private readonly IValidator<CreateGymCommand> _createGymValidator;
    private readonly IValidator<UpdateGymCommand> _updateGymValidator;
    private readonly IValidator<CreateGymBranchCommand> _createBranchValidator;
    private readonly IValidator<CreateGymRoomCommand> _createRoomValidator;

    public GymService(
        IAppDbContext context,
        IValidator<CreateGymCommand> createGymValidator,
        IValidator<UpdateGymCommand> updateGymValidator,
        IValidator<CreateGymBranchCommand> createBranchValidator,
        IValidator<CreateGymRoomCommand> createRoomValidator)
    {
        _context = context;
        _createGymValidator = createGymValidator;
        _updateGymValidator = updateGymValidator;
        _createBranchValidator = createBranchValidator;
        _createRoomValidator = createRoomValidator;
    }

    public async Task<Guid> CreateGymAsync(CreateGymCommand command, CancellationToken ct = default)
    {
        await _createGymValidator.ValidateAndThrowDomainAsync(command, ct);

        var gym = Gym.Create(command.Name, command.Description, command.TimeZone);
        
        _context.Gyms.Add(gym);
        await _context.SaveChangesAsync(ct);

        return gym.Id;
    }

    public async Task UpdateGymAsync(UpdateGymCommand command, CancellationToken ct = default)
    {
        await _updateGymValidator.ValidateAndThrowDomainAsync(command, ct);

        var gym = await _context.Gyms
            .FirstOrDefaultAsync(x => x.Id == command.Id, ct)
            ?? throw new NotFoundException(nameof(Gym), command.Id);

        gym.Update(command.Name, command.Description, command.TimeZone, command.IsActive);
        await _context.SaveChangesAsync(ct);
    }

    public async Task<Guid> CreateBranchAsync(CreateGymBranchCommand command, CancellationToken ct = default)
    {
        await _createBranchValidator.ValidateAndThrowDomainAsync(command, ct);

        var gym = await _context.Gyms
            .Include(x => x.Branches)
            .FirstOrDefaultAsync(x => x.Id == command.GymId, ct)
            ?? throw new NotFoundException(nameof(Gym), command.GymId);

        var branch = gym.AddBranch(command.Name, command.Address, command.PhoneNumber);
        await _context.SaveChangesAsync(ct);

        return branch.Id;
    }

    public async Task<Guid> CreateRoomAsync(CreateGymRoomCommand command, CancellationToken ct = default)
    {
        await _createRoomValidator.ValidateAndThrowDomainAsync(command, ct);

        var branch = await _context.GymBranches
            .Include(x => x.Rooms)
            .FirstOrDefaultAsync(x => x.Id == command.GymBranchId, ct)
            ?? throw new NotFoundException(nameof(GymBranch), command.GymBranchId);

        var room = branch.AddRoom(command.Name, command.Capacity);
        await _context.SaveChangesAsync(ct);

        return room.Id;
    }

    public async Task<GymDto> GetByIdAsync(Guid id, CancellationToken ct = default)
    {
        var gym = await _context.Gyms
            .AsNoTracking()
            .Include(g => g.Branches)
                .ThenInclude(b => b.Rooms)
            .FirstOrDefaultAsync(g => g.Id == id, ct)
            ?? throw new NotFoundException(nameof(Gym), id);

        return MapToDto(gym);
    }

    public async Task<IReadOnlyCollection<GymDto>> GetAllAsync(CancellationToken ct = default)
    {
        var gyms = await _context.Gyms
            .AsNoTracking()
            .Include(g => g.Branches)
                .ThenInclude(b => b.Rooms)
            .ToListAsync(ct);

        return gyms.Select(MapToDto).ToList();
    }

    public async Task DeleteGymAsync(Guid id, CancellationToken ct = default)
    {
        await _context.Gyms
            .Where(g=>g.Id == id)
            .ExecuteDeleteAsync(ct);
    }

    private static GymDto MapToDto(Gym gym) => new(
        gym.Id,
        gym.Name,
        gym.Description,
        gym.TimeZone,
        gym.IsActive,
        gym.CreatedAt,
        gym.Branches.Select(b => new GymBranchDto(
            b.Id,
            b.Name,
            b.Address,
            b.PhoneNumber,
            b.IsActive,
            b.Rooms.Select(r => new GymRoomDto(r.Id, r.Name, r.Capacity, r.IsActive)).ToList()
        )).ToList()
    );
}
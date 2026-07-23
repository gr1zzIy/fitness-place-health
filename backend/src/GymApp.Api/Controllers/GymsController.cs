using Asp.Versioning;
using GymApp.Application.Gyms;
using GymApp.Application.Gyms.Commands.CreateGym;
using GymApp.Application.Gyms.Commands.CreateGymBranch;
using GymApp.Application.Gyms.Commands.CreateGymRoom;
using GymApp.Application.Gyms.Commands.UpdateGym;
using GymApp.Application.Gyms.Queries.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class GymsController : ControllerBase
{
    private readonly IGymService _gymService;

    public GymsController(IGymService gymService)
    {
        _gymService = gymService;
    }

    [HttpPost]
    [ApiVersion(1)]
    public async Task<ActionResult<Guid>> CreateGym(
        [FromBody] CreateGymCommand command, 
        CancellationToken ct)
    {
        var gymId = await _gymService.CreateGymAsync(command, ct);
        return CreatedAtAction(nameof(GetGymById), new { id = gymId }, gymId);
    }

    [HttpGet]
    [ApiVersion(1)]
    public async Task<ActionResult<IReadOnlyCollection<GymDto>>> GetGyms(CancellationToken ct)
    {
        var gyms = await _gymService.GetAllAsync(ct);
        return Ok(gyms);
    }

    [HttpGet("{id:guid}")]
    [ApiVersion(1)]
    public async Task<ActionResult<GymDto>> GetGymById(Guid id, CancellationToken ct)
    {
        var gym = await _gymService.GetByIdAsync(id, ct);
        return Ok(gym);
    }

    [HttpPut("{id:guid}")]
    [ApiVersion(1)]
    public async Task<IActionResult> UpdateGym(
        Guid id, 
        [FromBody] UpdateGymCommand command, 
        CancellationToken ct)
    {
        if (id != command.Id)
            return BadRequest("ID у маршруті та тілі запиту не збігаються.");

        await _gymService.UpdateGymAsync(command, ct);
        return NoContent();
    }

    [HttpPost("{gymId:guid}/branches")]
    [ApiVersion(1)]
    public async Task<ActionResult<Guid>> CreateGymBranch(
        Guid gymId, 
        [FromBody] CreateGymBranchCommand command, 
        CancellationToken ct)
    {
        if (gymId != command.GymId)
            return BadRequest("GymId у маршруті та тілі запиту не збігаються.");

        var branchId = await _gymService.CreateBranchAsync(command, ct);
        return Ok(branchId);
    }

    [HttpPost("/api/branches/{branchId:guid}/rooms")]
    [ApiVersion(1)]
    public async Task<ActionResult<Guid>> CreateGymRoom(
        Guid branchId, 
        [FromBody] CreateGymRoomCommand command, 
        CancellationToken ct)
    {
        if (branchId != command.GymBranchId)
            return BadRequest("BranchId у маршруті та тілі запиту не збігаються.");

        var roomId = await _gymService.CreateRoomAsync(command, ct);
        return Ok(roomId);
    }

    [HttpDelete("{id:guid}")]
    [ApiVersion(1)]
    public async Task<IActionResult> DeleteGym(Guid id, CancellationToken ct)
    {
        await _gymService.DeleteGymAsync(id, ct);

        return NoContent();
    }
}
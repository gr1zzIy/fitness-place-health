using Asp.Versioning;
using GymApp.Application.Trainers;
using GymApp.Application.Trainers.Commands.AddTrainerSpecialization;
using GymApp.Application.Trainers.Commands.CreateTrainer;
using GymApp.Application.Trainers.Queries.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace GymApp.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TrainersController : ControllerBase
{
    private readonly ITrainerService _trainerService;

    public TrainersController(ITrainerService trainerService)
    {
        _trainerService = trainerService;
    }

    [HttpPost]
    [ApiVersion(1)]
    public async Task<ActionResult<Guid>> CreateTrainer(
        [FromBody] CreateTrainerCommand command, 
        CancellationToken ct)
    {
        var trainerId = await _trainerService.CreateTrainerAsync(command, ct);
        return CreatedAtAction(nameof(GetTrainerById), new { id = trainerId }, trainerId);
    }

    [HttpGet]
    [ApiVersion(1)]
    public async Task<ActionResult<IReadOnlyCollection<TrainerDto>>> GetTrainers(CancellationToken ct)
    {
        var trainers = await _trainerService.GetAllAsync(ct);
        return Ok(trainers);
    }

    [HttpGet("{id:guid}")]
    [ApiVersion(1)]
    public async Task<ActionResult<TrainerDto>> GetTrainerById(Guid id, CancellationToken ct)
    {
        var trainer = await _trainerService.GetByIdAsync(id, ct);
        return Ok(trainer);
    }

    [HttpPost("{id:guid}/specializations")]
    [ApiVersion(1)]
    public async Task<IActionResult> AddSpecialization(
        Guid id, 
        [FromBody] AddTrainerSpecializationCommand command, 
        CancellationToken ct)
    {
        if (id != command.TrainerProfileId)
            return BadRequest("TrainerProfileId у маршруті та тілі запиту не збігаються.");

        await _trainerService.AddSpecializationAsync(command, ct);
        return NoContent();
    }
}
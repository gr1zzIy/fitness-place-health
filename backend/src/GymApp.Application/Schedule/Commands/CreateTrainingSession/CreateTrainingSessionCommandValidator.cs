using FluentValidation;

namespace GymApp.Application.Schedule.Commands;

public sealed class CreateTrainingSessionCommandValidator 
    : AbstractValidator<CreateTrainingSessionCommand>
{
    public CreateTrainingSessionCommandValidator()
    {
        RuleFor(x => x.GymBranchId)
            .NotEmpty()
            .WithMessage("GymBranchId є обов'язковим.");
        
        RuleFor(x => x.GymRoomId)
            .NotEmpty()
            .WithMessage("GymRoomId є обов'язковим.");
        
        RuleFor(x => x.TrainingTypeId)
            .NotEmpty()
            .WithMessage("TrainingTypeId є обов'язковим.");
        
        RuleFor(x => x.TrainerProfileId)
            .NotEmpty()
            .WithMessage("TrainerProfileId є обов'язковим.");

        RuleFor(x => x.Capacity)
            .GreaterThan(0)
            .WithMessage("Місткість групи повинна бути більше 0.");

        RuleFor(x => x.StartsAt)
            .GreaterThan(DateTimeOffset.UtcNow)
            .WithMessage("Час початку тренування має бути у майбутньому.");

        RuleFor(x => x.EndsAt)
            .GreaterThan(x => x.StartsAt)
            .WithMessage("Час завершення має бути пізніше за час початку.");
    }
}
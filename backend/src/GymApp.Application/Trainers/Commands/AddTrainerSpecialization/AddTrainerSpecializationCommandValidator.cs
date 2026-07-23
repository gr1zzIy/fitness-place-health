using FluentValidation;

namespace GymApp.Application.Trainers.Commands.AddTrainerSpecialization;

public sealed class AddTrainerSpecializationCommandValidator : AbstractValidator<AddTrainerSpecializationCommand>
{
    public AddTrainerSpecializationCommandValidator()
    {
        RuleFor(x => x.TrainerProfileId)
            .NotEmpty();
        
        RuleFor(x => x.SpecializationId)
            .NotEmpty();
    }
}
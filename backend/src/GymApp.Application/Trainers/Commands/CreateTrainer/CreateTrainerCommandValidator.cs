using FluentValidation;

namespace GymApp.Application.Trainers.Commands.CreateTrainer;

public sealed class CreateTrainerCommandValidator : AbstractValidator<CreateTrainerCommand>
{
    public CreateTrainerCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
        
        RuleFor(x => x.Password)
            .NotEmpty()
            .MinimumLength(8);
        
        RuleFor(x => x.FirstName)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.LastName)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.ExperienceYears)
            .GreaterThanOrEqualTo(0);
    }
}
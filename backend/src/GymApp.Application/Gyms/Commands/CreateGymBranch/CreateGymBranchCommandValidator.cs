using FluentValidation;

namespace GymApp.Application.Gyms.Commands.CreateGymBranch;

public sealed class CreateGymBranchCommandValidator : AbstractValidator<CreateGymBranchCommand>
{
    public CreateGymBranchCommandValidator()
    {
        RuleFor(x => x.GymId).NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);
        
        RuleFor(x => x.Address)
            .NotEmpty()
            .MaximumLength(300);
    }
}
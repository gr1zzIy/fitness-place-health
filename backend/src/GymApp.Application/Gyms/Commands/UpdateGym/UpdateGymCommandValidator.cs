using FluentValidation;

namespace GymApp.Application.Gyms.Commands.UpdateGym;

public sealed class UpdateGymCommandValidator : AbstractValidator<UpdateGymCommand>
{
    public UpdateGymCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(150);
        
        RuleFor(x => x.TimeZone).NotEmpty();
    }
}
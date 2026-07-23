using FluentValidation;

namespace GymApp.Application.Gyms.Commands.CreateGymRoom;

public sealed class CreateGymRoomCommandValidator : AbstractValidator<CreateGymRoomCommand>
{
    public CreateGymRoomCommandValidator()
    {
        RuleFor(x => x.GymBranchId).NotEmpty();
        
        RuleFor(x => x.Name)
            .NotEmpty()
            .MaximumLength(100);
        
        RuleFor(x => x.Capacity)
            .GreaterThan(0)
            .WithMessage("Місткість повинна бути більше 0.");
    }
}
using FluentValidation;

namespace GymApp.Application.Gyms.Commands.CreateGym;

public sealed class CreateGymCommandValidator : AbstractValidator<CreateGymCommand>
{
    public CreateGymCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Назва спортзалу обов'язкова.")
            .MaximumLength(150)
            .WithMessage("Назва не повинна перевищувати 150 символів.");

        RuleFor(x => x.TimeZone)
            .NotEmpty()
            .WithMessage("Часовий пояс обов'язковий.");
    }
}
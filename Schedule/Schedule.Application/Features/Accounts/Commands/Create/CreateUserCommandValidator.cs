using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Users.Commands.Create;

public sealed class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Login)
            .MaximumLength(50);
        RuleFor(command => command.Password)
            .MaximumLength(100);
        RuleFor(command => command.RoleId)
            .SetValidator(new IdValidator());
    }
}
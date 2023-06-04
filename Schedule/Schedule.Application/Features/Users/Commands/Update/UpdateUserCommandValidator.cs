using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Users.Commands.Update;

public sealed class UpdateUserCommandValidator : AbstractValidator<UpdateUserCommand>
{
    public UpdateUserCommandValidator()
    {
        RuleFor(command => command.Id)
            .SetValidator(new IdValidator());
        RuleFor(command => command.Login)
            .MaximumLength(50);
        RuleFor(command => command.Password)
            .MaximumLength(100);
        RuleFor(command => command.RoleId)
            .SetValidator(new IdValidator());
    }
}
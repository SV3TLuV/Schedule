using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Accounts.Commands.Create;

public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(command => command.Login)
            .MaximumLength(50);
        RuleFor(command => command.Password)
            .MaximumLength(100);
        RuleFor(command => command.RoleId)
            .SetValidator(new IdValidator());
    }
}
using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Accounts.Commands.Create;

public sealed class CreateAccountCommandValidator : AbstractValidator<CreateAccountCommand>
{
    public CreateAccountCommandValidator()
    {
        RuleFor(command => command.Login)
            .Length(4, 50)
            .NotEmpty();
        RuleFor(command => command.Password)
            .Length(4, 100)
            .NotEmpty();
        RuleFor(command => command.RoleId)
            .SetValidator(new IdValidator());
        RuleFor(command => command.GroupId)
            .GreaterThan(0);
        RuleFor(command => command.Email)
            .EmailAddress()
            .NotEmpty()
            .Length(5, 200);
        RuleFor(command => command.Name)
            .Length(2, 40)
            .NotEmpty();
        RuleFor(command => command.Surname)
            .Length(2, 40)
            .NotEmpty();
        RuleFor(command => command.MiddleName)
            .Length(2, 40);
    }
}
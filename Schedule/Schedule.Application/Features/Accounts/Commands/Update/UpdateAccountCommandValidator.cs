using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Accounts.Commands.Update;

public sealed class UpdateAccountCommandValidator : AbstractValidator<UpdateAccountCommand>
{
    public UpdateAccountCommandValidator()
    {
        RuleFor(command => command.Id)
            .SetValidator(new IdValidator());
        RuleFor(command => command.RoleId)
            .GreaterThan(0);
        RuleFor(command => command.GroupId)
            .GreaterThan(0);
        RuleFor(command => command.Email)
            .EmailAddress()
            .Length(5, 200);
        RuleFor(command => command.Name)
            .Length(2, 40);
        RuleFor(command => command.Surname)
            .Length(2, 40);
        RuleFor(command => command.MiddleName)
            .Length(2, 40);
    }
}
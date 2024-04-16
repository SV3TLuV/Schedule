using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Accounts.Commands.UpdateEmployeePermission;

public class UpdateEmployeePermissionCommandValidator : AbstractValidator<UpdateEmployeePermissionCommand>
{
    public UpdateEmployeePermissionCommandValidator()
    {
        RuleFor(command => command.AccountId)
            .SetValidator(new IdValidator());
        RuleFor(command => command.PermissionIds)
            .SetValidator(new IdsValidator());
    }
}
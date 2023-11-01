using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineNames.Commands.Restore;

public class RestoreDisciplineNameCommandValidator : AbstractValidator<RestoreDisciplineNameCommand>
{
    public RestoreDisciplineNameCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
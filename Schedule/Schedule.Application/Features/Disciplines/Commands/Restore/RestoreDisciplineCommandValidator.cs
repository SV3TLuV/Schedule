using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Disciplines.Commands.Restore;

public class RestoreDisciplineCommandValidator : AbstractValidator<RestoreDisciplineCommand>
{
    public RestoreDisciplineCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Restore;

public sealed class RestoreDisciplineCodeCommandValidator : AbstractValidator<RestoreDisciplineCodeCommand>
{
    public RestoreDisciplineCodeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
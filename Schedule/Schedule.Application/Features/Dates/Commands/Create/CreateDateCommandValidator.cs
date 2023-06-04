using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Dates.Commands.Create;

public sealed class CreateDateCommandValidator : AbstractValidator<CreateDateCommand>
{
    public CreateDateCommandValidator()
    {
        RuleFor(command => command.Term)
            .InclusiveBetween(1, 2);
        RuleFor(query => query.DayId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.WeekTypeId)
            .SetValidator(new IdValidator());
    }
}
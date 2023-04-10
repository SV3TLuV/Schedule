using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.WeekTypes.Queries.Get;

public sealed class GetWeekTypeQueryValidator : AbstractValidator<GetWeekTypeQuery>
{
    public GetWeekTypeQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
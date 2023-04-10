using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Days.Queries.Get;

public sealed class GetDayQueryValidator : AbstractValidator<GetDayQuery>
{
    public GetDayQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
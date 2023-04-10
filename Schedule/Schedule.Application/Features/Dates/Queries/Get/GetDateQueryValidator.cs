using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Dates.Queries.Get;

public sealed class GetDateQueryValidator : AbstractValidator<GetDateQuery>
{
    public GetDateQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
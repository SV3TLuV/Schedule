using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.TimeTypes.Queries.Get;

public sealed class GetTimeTypeQueryValidator : AbstractValidator<GetTimeTypeQuery>
{
    public GetTimeTypeQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
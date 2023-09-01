using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Groups.Queries.GetAvailableForJoin;

public sealed class GetAvailableForJoinGroupQueryValidator : AbstractValidator<GetAvailableForJoinGroupQuery>
{
    public GetAvailableForJoinGroupQueryValidator()
    {
        RuleFor(query => query.TermId)
            .SetValidator(new IdValidator());
        RuleFor(query => query.SpecialityId)
            .SetValidator(new IdValidator());
    }
}
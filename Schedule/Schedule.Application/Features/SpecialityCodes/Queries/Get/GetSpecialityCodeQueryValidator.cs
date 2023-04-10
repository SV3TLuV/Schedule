using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.SpecialityCodes.Queries.Get;

public sealed class GetSpecialityCodeQueryValidator : AbstractValidator<GetSpecialityCodeQuery>
{
    public GetSpecialityCodeQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
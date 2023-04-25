using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Specialities.Queries.Get;

public sealed class GetSpecialityQueryValidator : AbstractValidator<GetSpecialityQuery>
{
    public GetSpecialityQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
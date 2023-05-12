using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineTypes.Queries.Get;

public sealed class GetDisciplineTypeQueryValidator : AbstractValidator<GetDisciplineTypeQuery>
{
    public GetDisciplineTypeQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
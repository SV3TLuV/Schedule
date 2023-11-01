using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineNames.Queries.Get;

public sealed class GetDisciplineNameQueryValidator : AbstractValidator<GetDisciplineNameQuery>
{
    public GetDisciplineNameQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
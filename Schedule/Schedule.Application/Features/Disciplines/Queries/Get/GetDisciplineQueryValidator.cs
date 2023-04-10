using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Disciplines.Queries.Get;

public sealed class GetDisciplineQueryValidator : AbstractValidator<GetDisciplineQuery>
{
    public GetDisciplineQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
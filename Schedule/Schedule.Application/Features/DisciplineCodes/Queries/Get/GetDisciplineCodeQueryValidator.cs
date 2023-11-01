using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineCodes.Queries.Get;

public sealed class GetDisciplineCodeQueryValidator : AbstractValidator<GetDisciplineCodeQuery>
{
    public GetDisciplineCodeQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
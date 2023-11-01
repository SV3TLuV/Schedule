using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.DisciplineNames.Queries.GetList;

public sealed class GetDisciplineNameQueryListValidator : AbstractValidator<GetDisciplineNameQueryList>
{
    public GetDisciplineNameQueryListValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
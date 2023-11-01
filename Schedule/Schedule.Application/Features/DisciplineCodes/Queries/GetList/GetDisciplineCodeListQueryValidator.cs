using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.DisciplineCodes.Queries.GetList;

public sealed class GetDisciplineCodeListQueryValidator : AbstractValidator<GetDisciplineCodeListQuery>
{
    public GetDisciplineCodeListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
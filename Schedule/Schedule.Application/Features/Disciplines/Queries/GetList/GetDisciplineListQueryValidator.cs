using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Disciplines.Queries.GetList;

public sealed class GetDisciplineListQueryValidator : AbstractValidator<GetDisciplineListQuery>
{
    public GetDisciplineListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
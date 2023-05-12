using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.DisciplineTypes.Queries.GetList;

public sealed class GetDisciplineTypeListQueryValidator : AbstractValidator<GetDisciplineTypeListQuery>
{
    public GetDisciplineTypeListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
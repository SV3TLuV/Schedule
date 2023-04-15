using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Teachers.Queries.GetList;

public sealed class GetTeacherListQueryValidator : AbstractValidator<GetTeacherListQuery>
{
    public GetTeacherListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
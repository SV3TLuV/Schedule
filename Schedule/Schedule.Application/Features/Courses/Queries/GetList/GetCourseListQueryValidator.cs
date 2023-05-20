using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Courses.Queries.GetList;

public sealed class GetCourseListQueryValidator : AbstractValidator<GetCourseListQuery>
{
    public GetCourseListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Lessons.Queries.GetList;

public sealed class GetLessonListQueryValidator : AbstractValidator<GetLessonListQuery>
{
    public GetLessonListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
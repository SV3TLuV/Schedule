using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Lessons.Queries.GetLessonNumberList;

public sealed class GetLessonNumberListQueryValidator : AbstractValidator<GetLessonNumberListQuery>
{
    public GetLessonNumberListQueryValidator()
    {
        RuleFor(query => query.DateId)
            .SetValidator(new IdValidator());
    }
}
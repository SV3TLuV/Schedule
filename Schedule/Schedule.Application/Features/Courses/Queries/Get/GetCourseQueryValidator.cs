using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Courses.Queries.Get;

public sealed class GetCourseQueryValidator : AbstractValidator<GetCourseQuery>
{
    public GetCourseQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
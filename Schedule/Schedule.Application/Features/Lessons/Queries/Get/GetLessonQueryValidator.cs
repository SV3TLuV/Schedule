using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Lessons.Queries.Get;

public sealed class GetLessonQueryValidator : AbstractValidator<GetLessonQuery>
{
    public GetLessonQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.LessonTemplates.Queries.Get;

public sealed class GetLessonTemplateQueryValidator : AbstractValidator<GetLessonTemplateQuery>
{
    public GetLessonTemplateQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
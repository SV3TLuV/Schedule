using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.LessonTemplates.Queries.GetList;

public sealed class GetLessonTemplateListQueryValidator
    : AbstractValidator<GetLessonTemplateListQuery>
{
    public GetLessonTemplateListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
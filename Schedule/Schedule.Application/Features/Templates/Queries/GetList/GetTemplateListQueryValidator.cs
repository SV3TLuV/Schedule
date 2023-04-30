using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Templates.Queries.GetList;

public sealed class GetTemplateListQueryValidator : AbstractValidator<GetTemplateListQuery>
{
    public GetTemplateListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
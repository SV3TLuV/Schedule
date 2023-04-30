using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Templates.Queries.Get;

public sealed class GetTemplateQueryValidator : AbstractValidator<GetTemplateQuery>
{
    public GetTemplateQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
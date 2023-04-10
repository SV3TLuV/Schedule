using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.SpecialityCodes.Queries.GetList;

public sealed class GetSpecialityCodeListQueryValidator : AbstractValidator<GetSpecialityCodeListQuery>
{
    public GetSpecialityCodeListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
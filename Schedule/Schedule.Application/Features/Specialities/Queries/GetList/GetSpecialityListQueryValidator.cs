using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Specialities.Queries.GetList;

public sealed class GetSpecialityListQueryValidator : AbstractValidator<GetSpecialityListQuery>
{
    public GetSpecialityListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
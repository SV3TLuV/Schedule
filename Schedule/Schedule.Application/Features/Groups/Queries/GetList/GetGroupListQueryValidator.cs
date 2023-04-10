using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public sealed class GetGroupListQueryValidator : AbstractValidator<GetGroupListQuery>
{
    public GetGroupListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
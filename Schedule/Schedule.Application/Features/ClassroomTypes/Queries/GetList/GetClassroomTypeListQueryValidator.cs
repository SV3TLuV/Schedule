using FluentValidation;
using Schedule.Application.Features.Base.Queries.Paginated;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed class GetClassroomTypeListQueryValidator : AbstractValidator<GetClassroomTypeListQuery>
{
    public GetClassroomTypeListQueryValidator()
    {
        RuleFor(query => query)
            .SetValidator(new PaginatedQueryValidator());
    }
}
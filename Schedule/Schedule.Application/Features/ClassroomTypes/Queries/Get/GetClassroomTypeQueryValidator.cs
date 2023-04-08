using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.ClassroomTypes.Queries.Get;

public sealed class GetClassroomTypeQueryValidator : AbstractValidator<GetClassroomTypeQuery>
{
    public GetClassroomTypeQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
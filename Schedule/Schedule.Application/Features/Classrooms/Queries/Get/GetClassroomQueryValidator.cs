using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Classrooms.Queries.Get;

public sealed class GetClassroomQueryValidator : AbstractValidator<GetClassroomQuery>
{
    public GetClassroomQueryValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
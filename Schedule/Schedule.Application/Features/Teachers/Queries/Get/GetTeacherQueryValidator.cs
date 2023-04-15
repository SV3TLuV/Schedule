using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Teachers.Queries.Get;

public sealed class GetTeacherQueryValidator : AbstractValidator<GetTeacherQuery>
{
    public GetTeacherQueryValidator()
    {
        RuleFor(x => x.Id)
            .SetValidator(new IdValidator());
    }
}
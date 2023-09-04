using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Groups.Queries.GetGroupDisciplines;

public sealed class GetGroupDisciplinesQueryValidator : AbstractValidator<GetGroupDisciplinesQuery>
{
    public GetGroupDisciplinesQueryValidator()
    {
        RuleFor(query => query.GroupId)
            .SetValidator(new IdValidator());
    }
}
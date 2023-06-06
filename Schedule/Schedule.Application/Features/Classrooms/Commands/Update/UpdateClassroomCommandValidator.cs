using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Classrooms.Commands.Update;

public class UpdateClassroomCommandValidator : AbstractValidator<UpdateClassroomCommand>
{
    public UpdateClassroomCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Cabinet)
            .MaximumLength(10)
            .NotEmpty();
    }
}
using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Update;

public class UpdateClassroomTypeCommandValidator : AbstractValidator<UpdateClassroomTypeCommand>
{
    public UpdateClassroomTypeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotNull();
    }
}
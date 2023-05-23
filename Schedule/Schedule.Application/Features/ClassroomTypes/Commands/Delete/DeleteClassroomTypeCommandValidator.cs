using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Delete;

public class DeleteClassroomTypeCommandValidator : AbstractValidator<DeleteClassroomTypeCommand>
{
    public DeleteClassroomTypeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
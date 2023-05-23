using FluentValidation;

namespace Schedule.Application.Features.ClassroomTypes.Commands.Create;

public class CreateClassroomTypeCommandValidator : AbstractValidator<CreateClassroomTypeCommand>
{
    public CreateClassroomTypeCommandValidator()
    {
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotNull();
    }
}
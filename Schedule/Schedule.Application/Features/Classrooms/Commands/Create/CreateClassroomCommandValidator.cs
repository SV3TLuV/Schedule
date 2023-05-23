using FluentValidation;

namespace Schedule.Application.Features.Classrooms.Commands.Create;

public class CreateClassroomCommandValidator : AbstractValidator<CreateClassroomCommand>
{
    public CreateClassroomCommandValidator()
    {
        RuleFor(query => query.Cabinet)
            .MaximumLength(10)
            .NotNull();
        RuleFor(query => query.TypeIds)
            .NotEmpty();
    }
}
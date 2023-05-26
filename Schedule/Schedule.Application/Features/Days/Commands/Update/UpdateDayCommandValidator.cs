using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.Days.Commands.Update;

public class UpdateDayCommandValidator : AbstractValidator<UpdateDayCommand>
{
    public UpdateDayCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
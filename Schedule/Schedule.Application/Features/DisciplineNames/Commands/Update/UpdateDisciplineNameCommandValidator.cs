using DocumentFormat.OpenXml.Office2010.Excel;
using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineNames.Commands.Update;

public class UpdateDisciplineNameCommandValidator : AbstractValidator<UpdateDisciplineNameCommand>
{
    public UpdateDisciplineNameCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Name)
            .MaximumLength(50)
            .NotEmpty();
    }
}
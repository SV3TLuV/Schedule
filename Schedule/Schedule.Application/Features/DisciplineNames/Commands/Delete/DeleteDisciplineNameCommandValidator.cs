using DocumentFormat.OpenXml.Office2010.Excel;
using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineNames.Commands.Delete;

public class DeleteDisciplineNameCommandValidator : AbstractValidator<DeleteDisciplineNameCommand>
{
    public DeleteDisciplineNameCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
    }
}
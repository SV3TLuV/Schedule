﻿using FluentValidation;
using Schedule.Application.Features.Base.Validators;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Update;

public sealed class UpdateDisciplineCodeCommandValidator : AbstractValidator<UpdateDisciplineCodeCommand>
{
    public UpdateDisciplineCodeCommandValidator()
    {
        RuleFor(query => query.Id)
            .SetValidator(new IdValidator());
        RuleFor(query => query.Code)
            .MaximumLength(20)
            .NotEmpty();
    }
}
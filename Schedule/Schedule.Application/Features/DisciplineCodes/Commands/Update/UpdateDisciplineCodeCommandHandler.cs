using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Update;

public sealed class UpdateDisciplineCodeCommandHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<UpdateDisciplineCodeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var disciplineCodeDbo = await context.DisciplineCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCodeDbo is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        var disciplineCode = mapper.Map<DisciplineCode>(request);
        context.DisciplineCodes.Update(disciplineCode);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
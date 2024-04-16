using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Update;

public sealed class UpdateDisciplineCommandHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<UpdateDisciplineCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var disciplineDbo = await context.Disciplines
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (disciplineDbo is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        var discipline = mapper.Map<Discipline>(request);
        context.Disciplines.Update(discipline);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
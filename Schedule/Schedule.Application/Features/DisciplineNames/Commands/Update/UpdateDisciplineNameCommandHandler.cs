using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Update;

public sealed class UpdateDisciplineNameCommandHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<UpdateDisciplineNameCommand, Unit>
{
    public async Task<Unit> Handle(UpdateDisciplineNameCommand request,
        CancellationToken cancellationToken)
    {
        var disciplineNameDbo = await context.DisciplineNames
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineNameId == request.Id, cancellationToken);

        if (disciplineNameDbo is null)
            throw new NotFoundException(nameof(DisciplineName), request.Id);

        var disciplineName = mapper.Map<DisciplineName>(request);
        context.DisciplineNames.Update(disciplineName);
        await context.SaveChangesAsync(cancellationToken);
        return Unit.Value;
    }
}
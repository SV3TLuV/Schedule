using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Commands.Create;

public sealed class CreateDisciplineCommandHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<CreateDisciplineCommand, int>
{
    public async Task<int> Handle(CreateDisciplineCommand request, CancellationToken cancellationToken)
    {
        var searched = await context.Disciplines
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e =>
                e.TermId == request.TermId &&
                e.NameId == request.NameId &&
                e.CodeId == request.CodeId &&
                e.SpecialityId == request.SpecialityId, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Дисциплина: {searched.Name}");
        
        var discipline = mapper.Map<Discipline>(request);
        await context.Disciplines.AddAsync(discipline, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return discipline.DisciplineId;
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineNames.Commands.Create;

public sealed class CreateDisciplineNameCommandHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<CreateDisciplineNameCommand, int>
{
    public async Task<int> Handle(CreateDisciplineNameCommand request,
        CancellationToken cancellationToken)
    {
        var searched = await context.DisciplineNames
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.Name == request.Name, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Название дисциплины: {searched.Name}");
        
        var disciplineCode = mapper.Map<DisciplineName>(request);
        await context.DisciplineNames.AddAsync(disciplineCode, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return disciplineCode.DisciplineNameId;
    }
}
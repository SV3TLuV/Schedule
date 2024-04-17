using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Commands.Create;

public sealed class CreateDisciplineCodeCommandHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<CreateDisciplineCodeCommand, int>
{
    public async Task<int> Handle(CreateDisciplineCodeCommand request, CancellationToken cancellationToken)
    {
        var searched = await context.DisciplineCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Code == request.Code, cancellationToken);

        if (searched is not null)
            throw new AlreadyExistsException($"Код дисциплины: {searched.Code}");
        
        var disciplineCode = mapper.Map<DisciplineCode>(request);
        await context.DisciplineCodes.AddAsync(disciplineCode, cancellationToken);
        await context.SaveChangesAsync(cancellationToken);
        return disciplineCode.DisciplineCodeId;
    }
}
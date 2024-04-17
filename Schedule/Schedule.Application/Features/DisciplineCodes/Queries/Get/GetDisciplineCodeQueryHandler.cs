using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineCodes.Queries.Get;

public sealed class GetDisciplineCodeQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetDisciplineCodeQuery, DisciplineCodeViewModel>
{
    public async Task<DisciplineCodeViewModel> Handle(GetDisciplineCodeQuery request,
        CancellationToken cancellationToken)
    {
        var disciplineCode = await context.DisciplineCodes
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineCodeId == request.Id, cancellationToken);

        if (disciplineCode is null)
            throw new NotFoundException(nameof(DisciplineCode), request.Id);

        return mapper.Map<DisciplineCodeViewModel>(disciplineCode);
    }
}
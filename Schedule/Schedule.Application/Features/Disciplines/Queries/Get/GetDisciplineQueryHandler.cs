using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Queries.Get;

public sealed class GetDisciplineQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetDisciplineQuery, DisciplineViewModel>
{
    public async Task<DisciplineViewModel> Handle(GetDisciplineQuery request, CancellationToken cancellationToken)
    {
        var discipline = await context.Disciplines
            .Include(e => e.Speciality)
            .Include(e => e.Name)
            .Include(e => e.Code)
            .Include(e => e.Type)
            .Include(e => e.Term)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        return mapper.Map<DisciplineViewModel>(discipline);
    }
}
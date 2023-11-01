using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Disciplines.Queries.Get;

public sealed class GetDisciplineQueryHandler : IRequestHandler<GetDisciplineQuery, DisciplineViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetDisciplineQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DisciplineViewModel> Handle(GetDisciplineQuery request, CancellationToken cancellationToken)
    {
        var discipline = await _context.Set<Discipline>()
            .Include(e => e.Name)
            .Include(e => e.Code)
            .Include(e => e.DisciplineType)
            .Include(e => e.Term)
            .ThenInclude(e => e.Course)
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.DisciplineId == request.Id, cancellationToken);

        if (discipline is null)
            throw new NotFoundException(nameof(Discipline), request.Id);

        return _mapper.Map<DisciplineViewModel>(discipline);
    }
}
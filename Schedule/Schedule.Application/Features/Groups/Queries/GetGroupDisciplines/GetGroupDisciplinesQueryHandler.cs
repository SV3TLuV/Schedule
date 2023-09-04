using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetGroupDisciplines;

public sealed class GetGroupDisciplinesQueryHandler : IRequestHandler<GetGroupDisciplinesQuery, ICollection<DisciplineViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupDisciplinesQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ICollection<DisciplineViewModel>> Handle(GetGroupDisciplinesQuery request,
        CancellationToken cancellationToken)
    {
        var group = await _context.Set<Group>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.GroupId == request.GroupId, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.GroupId); 
        
        var disciplines = await _context.Set<Discipline>()
            .Where(e => e.SpecialityId == group.SpecialityId)
            .Where(e => e.TermId == group.TermId)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
        return _mapper.Map<ICollection<DisciplineViewModel>>(disciplines);
    }
}
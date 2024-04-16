using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetGroupDisciplines;

public sealed class GetGroupDisciplinesQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetGroupDisciplinesQuery, ICollection<DisciplineViewModel>>
{
    public async Task<ICollection<DisciplineViewModel>> Handle(GetGroupDisciplinesQuery request,
        CancellationToken cancellationToken)
    {
        var group = await context.Groups
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.GroupId == request.GroupId, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.GroupId); 
        
        return await context.Disciplines
            .Include(e => e.Name)
            .Include(e => e.Code)
            .Where(e => e.SpecialityId == group.SpecialityId)
            .Where(e => e.TermId == group.TermId)
            .Where(e => !e.IsDeleted)
            .AsNoTracking()
            .ProjectTo<DisciplineViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
    }
}
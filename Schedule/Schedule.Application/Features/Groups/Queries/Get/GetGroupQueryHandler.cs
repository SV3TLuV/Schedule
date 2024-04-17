using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.Get;

public sealed class GetGroupQueryHandler(IScheduleDbContext context, IMapper mapper)
    : IRequestHandler<GetGroupQuery, GroupViewModel>
{
    public async Task<GroupViewModel> Handle(GetGroupQuery request, CancellationToken cancellationToken)
    {
        var group = await context.Groups
            .Include(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Speciality)
            .ThenInclude(e => e.Disciplines)
            .ThenInclude(e => e.Term)
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.GroupId == request.Id, cancellationToken);

        if (group is null)
            throw new NotFoundException(nameof(Group), request.Id);

        return mapper.Map<GroupViewModel>(group);
    }
}
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;

namespace Schedule.Application.Features.Groups.Queries.GetCourseGroups;

public class GetCourseGroupsQueryHandler(IScheduleDbContext context, IMapper mapper) :
    IRequestHandler<GetCourseGroupsQuery, ICollection<GroupedViewModel<SpecialityViewModel, GroupViewModel>>>
{
    public async Task<ICollection<GroupedViewModel<SpecialityViewModel, GroupViewModel>>> Handle(GetCourseGroupsQuery request, 
        CancellationToken cancellationToken)
    {
        var termId = request.course * 2;

        var query = context.Groups
            .Include(e => e.Speciality)
            .Where(e => e.TermId == termId ||
                        e.TermId == termId - 1)
            .AsNoTracking();

        var groups = await query
            .OrderBy(e => e.SpecialityId)
            .ThenBy(e => e.GroupId)
            .ProjectTo<GroupViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return groups
            .GroupBy(e => e.Speciality)
            .Select(grouping => new GroupedViewModel<SpecialityViewModel, GroupViewModel>
            {
                Key = grouping.Key,
                Items = grouping.ToList()
            })
            .ToList();
    }
}
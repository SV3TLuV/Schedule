using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Queries.GetLessonNumberList;

public sealed class GetLessonNumberListQueryHandler(IScheduleDbContext context)
    : IRequestHandler<GetLessonNumberListQuery, ICollection<int>>
{
    public async Task<ICollection<int>> Handle(GetLessonNumberListQuery request,
        CancellationToken cancellationToken)
    {
        return await context.Lessons
            .AsNoTracking()
            .Include(e => e.Timetable)
            .Where(e => e.Timetable.DateId == request.DateId)
            .Select(e => e.Number)
            .Distinct()
            .OrderBy(number => number)
            .ToListAsync(cancellationToken);
    }
}
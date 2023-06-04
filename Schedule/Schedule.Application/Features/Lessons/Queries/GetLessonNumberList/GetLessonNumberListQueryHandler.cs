using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Lessons.Queries.GetLessonNumberList;

public sealed class GetLessonNumberListQueryHandler
    : IRequestHandler<GetLessonNumberListQuery, ICollection<int>>
{
    private readonly IScheduleDbContext _context;

    public GetLessonNumberListQueryHandler(
        IScheduleDbContext context)
    {
        _context = context;
    }

    public async Task<ICollection<int>> Handle(GetLessonNumberListQuery request,
        CancellationToken cancellationToken)
    {
        return await _context.Set<Lesson>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Timetable)
            .Where(e => e.Timetable.DateId == request.DateId)
            .Select(e => e.Number)
            .Distinct()
            .OrderBy(number => number)
            .ToListAsync(cancellationToken);
    }
}
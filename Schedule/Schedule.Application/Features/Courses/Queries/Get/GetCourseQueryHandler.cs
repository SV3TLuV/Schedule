using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Exceptions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Courses.Queries.Get;

public sealed class GetCourseQueryHandler : IRequestHandler<GetCourseQuery, CourseViewModel>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetCourseQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<CourseViewModel> Handle(GetCourseQuery request,
        CancellationToken cancellationToken)
    {
        var course = await _context.Set<Course>()
            .AsNoTrackingWithIdentityResolution()
            .FirstOrDefaultAsync(e => e.CourseId == request.Id, cancellationToken);

        if (course is null)
            throw new NotFoundException(nameof(Course), request.Id);

        return _mapper.Map<CourseViewModel>(course);
    }
}
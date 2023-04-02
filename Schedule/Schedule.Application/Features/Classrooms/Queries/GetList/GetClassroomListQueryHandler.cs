using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.Classrooms.Queries.GetList;

public sealed class GetClassroomListQueryHandler
    : IRequestHandler<GetClassroomListQuery, ClassroomViewModel[]>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ClassroomViewModel[]> Handle(GetClassroomListQuery request,
        CancellationToken cancellationToken)
    {
        var classrooms = await _context.Classrooms
            .Include(e => e.ClassroomTypes)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);
        return _mapper.Map<ClassroomViewModel[]>(classrooms);
    }
}
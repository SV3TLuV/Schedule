using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed class GetClassroomTypeListQueryHandler
    : IRequestHandler<GetClassroomTypeListQuery, ClassroomTypeViewModel[]>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomTypeListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<ClassroomTypeViewModel[]> Handle(GetClassroomTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var classroomTypes = await _context.ClassroomTypes
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);
        return _mapper.Map<ClassroomTypeViewModel[]>(classroomTypes);
    }
}
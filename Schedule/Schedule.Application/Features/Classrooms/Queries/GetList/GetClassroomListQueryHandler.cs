using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.Classrooms.Queries.GetList;

public sealed class GetClassroomListQueryHandler
    : IRequestHandler<GetClassroomListQuery, PagedList<ClassroomViewModel>>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<ClassroomViewModel>> Handle(GetClassroomListQuery request,
        CancellationToken cancellationToken)
    {
        var classrooms = await _context.Classrooms
            .Include(e => e.ClassroomTypes)
            .AsNoTrackingWithIdentityResolution()
            .Skip((request.Page - 1) * request.Count)
            .Take(request.Count)
            .ToListAsync(cancellationToken);
        
        var viewModels = _mapper.Map<ClassroomViewModel[]>(classrooms);
        var totalCount = await _context.ClassroomTypes.CountAsync(cancellationToken);
        
        return new PagedList<ClassroomViewModel>
        {
            PageSize = request.Count,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
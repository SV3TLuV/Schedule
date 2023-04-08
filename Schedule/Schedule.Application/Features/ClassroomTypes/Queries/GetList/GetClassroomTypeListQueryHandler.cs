using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;
using Schedule.Persistence.Context;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed class GetClassroomTypeListQueryHandler
    : IRequestHandler<GetClassroomTypeListQuery, PagedList<ClassroomTypeViewModel>>
{
    private readonly ScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomTypeListQueryHandler(ScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<PagedList<ClassroomTypeViewModel>> Handle(GetClassroomTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var classroomTypes = await _context.ClassroomTypes
            .AsNoTrackingWithIdentityResolution()
            .Skip((request.Page - 1) * request.Count)
            .Take(request.Count)
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<ClassroomTypeViewModel[]>(classroomTypes);
        var totalCount = await _context.ClassroomTypes.CountAsync(cancellationToken);

        return new PagedList<ClassroomTypeViewModel>
        {
            PageSize = request.Count,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
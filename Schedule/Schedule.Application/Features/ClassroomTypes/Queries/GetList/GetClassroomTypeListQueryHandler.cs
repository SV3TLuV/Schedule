using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed class GetClassroomTypeListQueryHandler
    : IRequestHandler<GetClassroomTypeListQuery, PagedList<ClassroomTypeViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetClassroomTypeListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<ClassroomTypeViewModel>> Handle(GetClassroomTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var classroomTypes = await _context.Set<ClassroomType>()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);

        var viewModels = _mapper.Map<ClassroomTypeViewModel[]>(classroomTypes);
        var totalCount = await _context.Set<ClassroomType>().CountAsync(cancellationToken);

        return new PagedList<ClassroomTypeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Roles.Queries.GetList;

public sealed class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, PagedList<RoleViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetRoleListQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<RoleViewModel>> Handle(GetRoleListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Role>()
            .AsNoTrackingWithIdentityResolution();

        var roles = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var totalCount = await query.CountAsync(cancellationToken);
        var viewModels = _mapper.Map<List<RoleViewModel>>(roles);

        return new PagedList<RoleViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
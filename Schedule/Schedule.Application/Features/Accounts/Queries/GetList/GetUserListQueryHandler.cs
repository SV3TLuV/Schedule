using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Users.Queries.GetList;

public sealed class GetUserListQueryHandler : IRequestHandler<GetUserListQuery, PagedList<UserViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetUserListQueryHandler(
        IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<UserViewModel>> Handle(GetUserListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<User>()
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.Role);

        var users = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var totalCount = await query.CountAsync(cancellationToken);
        var viewModels = _mapper.Map<List<UserViewModel>>(users);

        return new PagedList<UserViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
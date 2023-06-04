using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Roles.Queries.GetList;

public sealed class GetRoleListQueryHandler : IRequestHandler<GetRoleListQuery, ICollection<RoleViewModel>>
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

    public async Task<ICollection<RoleViewModel>> Handle(GetRoleListQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await _context.Set<Role>()
            .AsNoTrackingWithIdentityResolution()
            .ToListAsync(cancellationToken);
        return _mapper.Map<List<RoleViewModel>>(roles);
    }
}
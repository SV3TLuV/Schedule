using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Roles.Queries.GetList;

public sealed class GetRoleListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetRoleListQuery, PagedList<RoleViewModel>>
{
    public async Task<PagedList<RoleViewModel>> Handle(GetRoleListQuery request,
        CancellationToken cancellationToken)
    {
        var roles = await context.Roles
            .AsNoTracking()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<RoleViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await context.Roles.CountAsync(cancellationToken);

        return new PagedList<RoleViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = roles
        };
    }
}
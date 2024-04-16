using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Queries.GetList;

public sealed class GetAccountListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetAccountListQuery, PagedList<AccountViewModel>>
{
    public async Task<PagedList<AccountViewModel>> Handle(GetAccountListQuery request,
        CancellationToken cancellationToken)
    {
        var accounts = await context.Accounts
            .AsNoTracking()
            .Include(e => e.Role)
            .Include(e => e.Employees)
            .Include(e => e.Students)
            .Include(e => e.Teachers)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<AccountViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await context.Accounts.CountAsync(cancellationToken);

        return new PagedList<AccountViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = accounts
        };
    }
}
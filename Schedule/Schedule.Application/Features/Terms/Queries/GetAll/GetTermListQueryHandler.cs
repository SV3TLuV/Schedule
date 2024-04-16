using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Terms.Queries.GetAll;

public sealed class GetTermListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetTermListQuery, PagedList<TermViewModel>>
{
    public async Task<PagedList<TermViewModel>> Handle(GetTermListQuery request,
        CancellationToken cancellationToken)
    {
        var terms = await context.Terms
            .AsNoTrackingWithIdentityResolution()
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ProjectTo<TermViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await context.Terms.CountAsync(cancellationToken);

        return new PagedList<TermViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = terms
        };
    }
}
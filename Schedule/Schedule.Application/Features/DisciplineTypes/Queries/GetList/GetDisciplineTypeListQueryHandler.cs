using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.DisciplineTypes.Queries.GetList;

public sealed class GetDisciplineTypeListQueryHandler(
    IScheduleDbContext context,
    IMapper mapper) : IRequestHandler<GetDisciplineTypeListQuery, PagedList<DisciplineTypeViewModel>>
{
    public async Task<PagedList<DisciplineTypeViewModel>> Handle(GetDisciplineTypeListQuery request,
        CancellationToken cancellationToken)
    {
        var disciplines = await context.DisciplineTypes
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTracking()
            .ProjectTo<DisciplineTypeViewModel>(mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        var totalCount = await context.DisciplineTypes.CountAsync(cancellationToken);

        return new PagedList<DisciplineTypeViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = disciplines
        };
    }
}
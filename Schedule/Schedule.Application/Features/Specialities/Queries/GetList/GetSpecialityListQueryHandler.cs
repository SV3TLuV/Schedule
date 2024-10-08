﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Specialities.Queries.GetList;

public sealed class GetSpecialityListQueryHandler
    : IRequestHandler<GetSpecialityListQuery, PagedList<SpecialityViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetSpecialityListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<SpecialityViewModel>> Handle(GetSpecialityListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Speciality>()
            .Include(e => e.Disciplines)
            .Include(e => e.Disciplines)
            .OrderBy(e => e.SpecialityId)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e =>
                e.Name.StartsWith(request.Search) ||
                e.Code.StartsWith(request.Search));

        var specialities = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<SpecialityViewModel[]>(specialities);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<SpecialityViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
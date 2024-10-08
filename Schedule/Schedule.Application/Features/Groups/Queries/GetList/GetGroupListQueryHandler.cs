﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Queries.GetList;

public sealed class GetGroupListQueryHandler
    : IRequestHandler<GetGroupListQuery, PagedList<GroupViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetGroupListQueryHandler(IScheduleDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<GroupViewModel>> Handle(GetGroupListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Group>()
            .Include(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Speciality)
            .ThenInclude(e => e.Disciplines)
            .Include(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.GroupGroups1)
            .ThenInclude(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .OrderBy(e => e.Term.CourseId)
            .ThenBy(e => e.Speciality.Code)
            .AsSplitQuery()
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        if (request.Search is not null)
            query = query.Where(e =>
                (e.Speciality.Name + '-' + e.Number).StartsWith(request.Search) ||
                e.Speciality.Code.StartsWith(request.Search) ||
                e.Number.StartsWith(request.Search));

        var groups = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<GroupViewModel[]>(groups);
        var totalCount = await query.CountAsync(cancellationToken);

        return new PagedList<GroupViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
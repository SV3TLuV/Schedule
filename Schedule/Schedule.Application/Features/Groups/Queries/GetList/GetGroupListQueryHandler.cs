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
            .Include(e => e.SpecialityCode)
            .ThenInclude(e => e.Disciplines)
            .Include(e => e.Course)
            .OrderBy(e => e.SpecialityCode.Code)
            .Skip((request.Page - 1) * request.Count)
            .Take(request.Count)
            .AsNoTrackingWithIdentityResolution();
        
        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };;
        
        var groups = await query.ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<GroupViewModel[]>(groups);
        var totalCount = await _context.Set<Group>().CountAsync(cancellationToken);

        return new PagedList<GroupViewModel>
        {
            PageSize = request.Count,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Queries.GetList;

public sealed class GetTeacherListQueryHandler
    : IRequestHandler<GetTeacherListQuery, PagedList<TeacherViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTeacherListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TeacherViewModel>> Handle(GetTeacherListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Teacher>()
            .Include(e => e.TeacherGroups)
            .Include(e => e.TeacherDisciplines)
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .AsNoTrackingWithIdentityResolution();

        query = request.Filter switch
        {
            QueryFilter.Available => query.Where(e => !e.IsDeleted),
            QueryFilter.Deleted => query.Where(e => e.IsDeleted),
            _ => query
        };

        var teachers = await query.ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<TeacherViewModel[]>(teachers);
        var totalCount = await _context.Set<Teacher>().CountAsync(cancellationToken);

        return new PagedList<TeacherViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
﻿using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Queries.GetList;

public sealed class GetTemplateListQueryHandler
    : IRequestHandler<GetTemplateListQuery, PagedList<TemplateViewModel>>
{
    private readonly IScheduleDbContext _context;
    private readonly IMapper _mapper;

    public GetTemplateListQueryHandler(IScheduleDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PagedList<TemplateViewModel>> Handle(GetTemplateListQuery request,
        CancellationToken cancellationToken)
    {
        var query = _context.Set<Template>()
            .Include(e => e.Day)
            .Include(e => e.Term)
            .Include(e => e.WeekType)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.GroupGroups)
            .ThenInclude(e => e.Group2)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.Group)
            .ThenInclude(e => e.Speciality)
            .Include(e => e.Group)
            .ThenInclude(e => e.Term)
            .ThenInclude(e => e.Course)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.Discipline)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.Time)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Teacher)
            .Include(e => e.LessonTemplates)
            .ThenInclude(e => e.LessonTemplateTeacherClassrooms)
            .ThenInclude(e => e.Classroom)
            .AsNoTrackingWithIdentityResolution();

        if (request.WeekTypeId is not null)
        {
            query = query.Where(e => e.WeekTypeId == request.WeekTypeId);
        }
        
        if (request.TermId is not null)
        {
            query = query.Where(e => e.TermId == request.TermId);
        }
        
        if (request.DayId is not null)
        {
            query = query.Where(e => e.DayId == request.DayId);
        }
        
        if (request.GroupId is not null)
        {
            query = query.Where(e => e.GroupId == request.GroupId);
        }
        
        var templates = await query
            .Skip((request.Page - 1) * request.PageSize)
            .Take(request.PageSize)
            .ToListAsync(cancellationToken);
        var viewModels = _mapper.Map<List<TemplateViewModel>>(templates);
        var totalCount = await query.CountAsync(cancellationToken);
        
        foreach (var viewModel in viewModels)
        {
            if (viewModel.Groups.Count <= 1)
                continue;

            for (var i = 1; i < viewModel.Groups.Count; i++)
            {
                var group = viewModel.Groups.ElementAt(i);
                var timetable = viewModels.First(v =>
                    v.Groups.First().Id == group.Id);
                viewModels.Remove(timetable);
            }

            viewModel.Groups = viewModel.Groups
                .OrderBy(e => e.Speciality.Code)
                .ToArray();
        }
        
        return new PagedList<TemplateViewModel>
        {
            PageSize = request.PageSize,
            PageNumber = request.Page,
            TotalCount = totalCount,
            Items = viewModels
        };
    }
}
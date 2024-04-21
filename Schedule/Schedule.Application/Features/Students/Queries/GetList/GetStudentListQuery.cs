﻿using MediatR;
using Schedule.Application.Common.Enums;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Students.Queries.GetList;

public sealed record GetStudentListQuery : PaginatedQuery, IRequest<PagedList<StudentViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
    public string? Search { get; set; }
}
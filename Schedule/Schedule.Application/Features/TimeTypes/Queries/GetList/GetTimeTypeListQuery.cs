﻿using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Common.Enums;
using Schedule.Core.Models;

namespace Schedule.Application.Features.TimeTypes.Queries.GetList;

public sealed record GetTimeTypeListQuery : PaginatedQuery, IRequest<PagedList<TimeTypeViewModel>>
{
    public required QueryFilter Filter { get; init; } = QueryFilter.Available;
}
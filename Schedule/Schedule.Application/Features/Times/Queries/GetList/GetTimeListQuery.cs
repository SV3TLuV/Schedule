﻿using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Times.Queries.GetList;

public sealed record GetTimeListQuery : PaginatedQuery, IRequest<PagedList<TimeViewModel>>;
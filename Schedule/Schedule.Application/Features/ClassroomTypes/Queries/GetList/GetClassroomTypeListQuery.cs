using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.ClassroomTypes.Queries.GetList;

public sealed record GetClassroomTypeListQuery
    : PaginatedQuery, IRequest<PagedList<ClassroomTypeViewModel>>;
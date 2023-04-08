using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Classrooms.Queries.GetList;

public sealed record GetClassroomListQuery : PaginatedQuery, IRequest<PagedList<ClassroomViewModel>>;
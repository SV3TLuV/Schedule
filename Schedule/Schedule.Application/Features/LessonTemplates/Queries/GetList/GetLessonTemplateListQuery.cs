using MediatR;
using Schedule.Application.Features.Base.Queries.Paginated;
using Schedule.Application.ViewModels;
using Schedule.Core.Models;

namespace Schedule.Application.Features.LessonTemplates.Queries.GetList;

public sealed record GetLessonTemplateListQuery
    : PaginatedQuery, IRequest<PagedList<LessonTemplateViewModel>>;
using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Disciplines.Queries.GetList;

public sealed record GetDisciplineListQuery : IRequest<DisciplineViewModel[]>;
using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Disciplines.Queries.Get;

public sealed record GetDisciplineQuery(int Id) : IRequest<DisciplineViewModel>;
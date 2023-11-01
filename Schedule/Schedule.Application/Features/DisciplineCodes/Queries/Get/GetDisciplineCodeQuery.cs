using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.DisciplineCodes.Queries.Get;

public sealed record GetDisciplineCodeQuery(int Id) : IRequest<DisciplineCodeViewModel>;
using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.DisciplineNames.Queries.Get;

public sealed record GetDisciplineNameQuery(int Id) : IRequest<DisciplineNameViewModel>;
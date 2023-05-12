using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.DisciplineTypes.Queries.Get;

public sealed record GetDisciplineTypeQuery(int Id) : IRequest<DisciplineTypeViewModel>;
using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.SpecialityCodes.Queries.Get;

public sealed record GetSpecialityCodeQuery(int Id) : IRequest<SpecialityCodeViewModel>;
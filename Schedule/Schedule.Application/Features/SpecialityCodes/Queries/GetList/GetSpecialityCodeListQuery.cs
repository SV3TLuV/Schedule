using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.SpecialityCodes.Queries.GetList;

public sealed record GetSpecialityCodeListQuery : IRequest<SpecialityCodeViewModel[]>;
using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Specialities.Queries.Get;

public sealed record GetSpecialityQuery(int Id) : IRequest<SpecialityViewModel>;
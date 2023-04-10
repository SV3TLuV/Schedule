using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Dates.Queries.GetCurrent;

public sealed record GetCurrentDateQuery : IRequest<DateViewModel>;
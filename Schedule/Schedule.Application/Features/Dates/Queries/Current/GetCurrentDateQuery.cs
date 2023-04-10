using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Dates.Queries.Current;

public sealed record GetCurrentDateQuery : IRequest<DateViewModel>;
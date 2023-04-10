using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Dates.Queries.Get;

public sealed record GetDateQuery(int Id) : IRequest<DateViewModel>;
using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Terms.Queries.Get;

public sealed record GetTermQuery(int Id) : IRequest<TermViewModel>;
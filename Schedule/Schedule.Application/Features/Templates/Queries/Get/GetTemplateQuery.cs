using MediatR;
using Schedule.Application.ViewModels;

namespace Schedule.Application.Features.Templates.Queries.Get;

public sealed record GetTemplateQuery(int Id) : IRequest<TemplateViewModel>;
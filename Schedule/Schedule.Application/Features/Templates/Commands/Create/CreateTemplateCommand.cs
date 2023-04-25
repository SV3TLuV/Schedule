using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Templates.Commands.Create;

public sealed class CreateTemplateCommand : IRequest<int>, IMapWith<Template>
{
    public int GroupId { get; set; }

    public int TermId { get; set; }

    public int DayId { get; set; }

    public int WeekTypeId { get; set; }
}
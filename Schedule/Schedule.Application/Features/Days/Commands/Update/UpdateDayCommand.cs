using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Commands.Update;

public sealed class UpdateDayCommand : IRequest, IMapWith<Day>
{
    public required int Id { get; set; }
    public required bool IsStudy { get; set; }
}
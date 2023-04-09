using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Update;

public sealed class UpdateSpecialityCodeCommand : IRequest, IMapWith<SpecialityCode>
{
    public required int Id { get; set; }
    public required string Code { get; set; }
}
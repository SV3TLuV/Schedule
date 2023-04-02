﻿using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.SpecialityCodes.Commands.Create;

public sealed class CreateSpecialityCodeCommand : IRequest<int>, IMapWith<SpecialityCode>
{
    public required string Code { get; set; }
}
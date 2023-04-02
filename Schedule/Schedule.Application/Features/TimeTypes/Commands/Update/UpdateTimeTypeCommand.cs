﻿using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.Features.TimeTypes.Commands.Update;

public sealed class UpdateTimeTypeCommand : IRequest, IMapWith<TimeType>
{
    public required int Id { get; set; }
    public required string Name { get; set; }
}
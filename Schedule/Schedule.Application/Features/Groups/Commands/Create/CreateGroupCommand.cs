﻿using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Groups.Commands.Create;

public sealed class CreateGroupCommand : IRequest<int>, IMapWith<Group>
{
    public required string Number { get; set; }
    public required int EnrollmentYear { get; set; }
    public required int SpecialityId { get; set; }
    public bool IsAfterEleven { get; set; }
    public required int TermId { get; set; }
}
﻿using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Days.Commands.Update;

public sealed class UpdateDayCommand : IRequest<Unit>, IMapWith<Day>
{
    public required int Id { get; init; }

    public required bool IsStudy { get; init; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Day, UpdateDayCommand>()
            .ForMember(command => command.Id, expression =>
                expression.MapFrom(day => day.DayId));

        profile.CreateMap<UpdateDayCommand, Day>()
            .ForMember(day => day.DayId, expression =>
                expression.MapFrom(command => command.Id));
    }
}
﻿using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TimetableViewModel : IMapWith<Timetable>
{
    public int Id { get; set; }

    public DateViewModel Date { get; set; } = null!;

    public ICollection<GroupViewModel> Groups { get; set; } = null!;

    public ICollection<LessonViewModel> Lessons { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Timetable, TimetableViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(timetable => timetable.TimetableId))
            .ForMember(viewModel => viewModel.Groups, expression =>
                expression.MapFrom(timetable => new[] { timetable.Group }));
        
        profile.CreateMap<TimetableViewModel, Timetable>()
            .ForMember(timetable => timetable.TimetableId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(timetable => timetable.Group, expression =>
                expression.MapFrom(viewModel => viewModel.Groups.First()));
    }
}
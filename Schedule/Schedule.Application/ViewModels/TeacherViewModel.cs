﻿using AutoMapper;
using Schedule.Core.Common.Extensions;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TeacherViewModel : IMapWith<Teacher>
{
    public int Id { get; set; }

    public AccountViewModel Account { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, TeacherViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(teacher => teacher.TeacherId));

        profile.CreateMap<TeacherViewModel, Teacher>()
            .ForMember(teacher => teacher.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Id));
    }
}
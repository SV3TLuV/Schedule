﻿using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class TermViewModel : IMapWith<Term>
{
    public int Id { get; set; }

    public int CourseTerm { get; set; }

    public Course Course { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Term, TermViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(term => term.TermId))
            .ReverseMap();
    }
}
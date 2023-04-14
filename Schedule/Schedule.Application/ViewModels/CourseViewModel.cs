using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class CourseViewModel : IMapWith<Course>
{
    public int Value { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Course, CourseViewModel>()
            .ForMember(viewModel => viewModel.Value, expression =>
                expression.MapFrom(course => course.CourseId))
            .ReverseMap();
    }
}
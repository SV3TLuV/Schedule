using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class CourseViewModel : IMapWith<Course>
{
    public int Id { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Course, CourseViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(course => course.CourseId))
            .ReverseMap();
    }
}
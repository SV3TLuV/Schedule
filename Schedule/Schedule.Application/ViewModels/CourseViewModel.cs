using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class CourseViewModel : IMapWith<Course>, IEquatable<CourseViewModel>
{
    public int Id { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Course, CourseViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(course => course.CourseId))
            .ReverseMap();
    }

    public bool Equals(CourseViewModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((CourseViewModel)obj);
    }

    public override int GetHashCode()
    {
        return Id;
    }
}
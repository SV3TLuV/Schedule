using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TermViewModel : IMapWith<Term>
{
    public int Value { get; set; }

    public int CourseTerm { get; set; }

    public CourseViewModel Course { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Term, TermViewModel>()
            .ForMember(viewModel => viewModel.Value, expression =>
                expression.MapFrom(term => term.TermId))
            .ReverseMap();
    }
}
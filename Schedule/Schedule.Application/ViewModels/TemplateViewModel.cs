using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class TemplateViewModel : IMapWith<Template>
{
    public int Id { get; set; }
    
    public int Term { get; set; }

    public DayViewModel Day { get; set; } = null!;

    public GroupViewModel Group { get; set; } = null!;

    public WeekTypeViewModel WeekType { get; set; } = null!;
    
    public ICollection<LessonViewModel> Lessons { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Template, TemplateViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(template => template.TemplateId))
            .ReverseMap();
    }
}
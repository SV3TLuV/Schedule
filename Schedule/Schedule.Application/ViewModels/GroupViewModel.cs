using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Entities;

namespace Schedule.Application.ViewModels;

public class GroupViewModel : IMapWith<Group>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;
    
    public int EnrollmentYear { get; set; }

    public bool IsDeleted { get; set; }

    public CourseViewModel Course { get; set; } = null!;

    public SpecialityCodeViewModel SpecialityCode { get; set; } = null!;
    
    public void Map(Profile profile)
    {
        profile.CreateMap<Group, GroupViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(group => group.GroupId))
            .ReverseMap();
    }
}
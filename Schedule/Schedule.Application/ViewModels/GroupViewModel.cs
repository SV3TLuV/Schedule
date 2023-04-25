using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class GroupViewModel : IMapWith<Group>
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public string Name => $"{Speciality.Name}-{Number}";

    public int EnrollmentYear { get; set; }

    public bool IsDeleted { get; set; }

    public CourseViewModel Course { get; set; } = null!;

    public SpecialityViewModel Speciality { get; set; } = null!;

    public ICollection<GroupViewModel> MergedGroups { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Group, GroupViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(group => group.GroupId))
            .ForMember(viewModel => viewModel.MergedGroups, expression =>
                expression.MapFrom(group => group.GroupGroups
                    .Select(gg => gg.Group2)));
        
        profile.CreateMap<GroupViewModel, Group>()
            .ForMember(group => group.GroupId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(group => group.GroupGroups, expression =>
                expression.MapFrom(viewModel => viewModel.MergedGroups
                    .Select(g => new GroupGroup
                    {
                        GroupId = viewModel.Id,
                        GroupId2 = g.Id,
                    })));
    }
}
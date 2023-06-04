using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class GroupViewModel : IMapWith<Group>, IEquatable<GroupViewModel>
{
    public int Id { get; set; }

    public string Number { get; set; } = null!;

    public string Name => Speciality is not null
        ? $"{Speciality.Name}-{Number}"
        : "";

    public int EnrollmentYear { get; set; }

    public bool IsDeleted { get; set; }

    public TermViewModel Term { get; set; } = null!;

    public SpecialityViewModel Speciality { get; set; } = null!;

    public ICollection<GroupViewModel> MergedGroups { get; set; } = null!;

    public bool Equals(GroupViewModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id &&
               Number == other.Number &&
               EnrollmentYear == other.EnrollmentYear &&
               IsDeleted == other.IsDeleted &&
               Term.Equals(other.Term) &&
               Speciality.Equals(other.Speciality) &&
               MergedGroups.Equals(other.MergedGroups);
    }

    public void Map(Profile profile)
    {
        profile.CreateMap<Group, GroupViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(group => group.GroupId))
            .ForMember(viewModel => viewModel.MergedGroups, expression =>
                expression.MapFrom(group => group.GroupGroups
                    .Select(gg => gg.Group2)
                    .Concat(group.GroupGroups1
                        .Select(gg => gg.Group))
                    .DistinctBy(g => g.GroupId)));

        profile.CreateMap<GroupViewModel, Group>()
            .ForMember(group => group.GroupId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(group => group.GroupGroups, expression =>
                expression.MapFrom(viewModel => viewModel.MergedGroups
                    .Select(g => new GroupGroup
                    {
                        GroupId = viewModel.Id,
                        GroupId2 = g.Id
                    })));
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((GroupViewModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Number, EnrollmentYear, IsDeleted, Term, Speciality, MergedGroups);
    }
}
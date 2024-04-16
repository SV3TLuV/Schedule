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
    
    public bool IsAfterEleven { get; set; }

    public TermViewModel Term { get; set; } = null!;

    public SpecialityViewModel Speciality { get; set; } = null!;

    public bool Equals(GroupViewModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id &&
               Number == other.Number &&
               EnrollmentYear == other.EnrollmentYear &&
               IsDeleted == other.IsDeleted &&
               Term.Equals(other.Term) &&
               Speciality.Equals(other.Speciality);
    }

    public void Map(Profile profile)
    {
        profile.CreateMap<Group, GroupViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(group => group.GroupId));

        profile.CreateMap<GroupViewModel, Group>()
            .ForMember(group => group.GroupId, expression =>
                expression.MapFrom(viewModel => viewModel.Id));
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
        return HashCode.Combine(Id, Number, EnrollmentYear, IsDeleted, Term, Speciality);
    }
}
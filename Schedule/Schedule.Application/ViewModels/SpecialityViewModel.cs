using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class SpecialityViewModel : IMapWith<Speciality>, IEquatable<SpecialityViewModel>
{
    public int Id { get; set; }

    public string Code { get; set; } = null!;

    public string Name { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public ICollection<DisciplineViewModel> Disciplines { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Speciality, SpecialityViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(speciality => speciality.SpecialityId))
            .ReverseMap();
    }

    public bool Equals(SpecialityViewModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id && 
               Code == other.Code &&
               Name == other.Name &&
               IsDeleted == other.IsDeleted && 
               Disciplines.Equals(other.Disciplines);
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((SpecialityViewModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Code, Name, IsDeleted, Disciplines);
    }
}
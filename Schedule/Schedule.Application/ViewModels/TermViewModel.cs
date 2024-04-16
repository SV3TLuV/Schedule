using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TermViewModel : IMapWith<Term>, IEquatable<TermViewModel>
{
    public int Id { get; set; }

    public int Course { get; set; }

    public bool Equals(TermViewModel? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Id == other.Id &&
               Course.Equals(other.Course);
    }

    public void Map(Profile profile)
    {
        profile.CreateMap<Term, TermViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(term => term.TermId))
            .ReverseMap();
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((TermViewModel)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Course);
    }
}
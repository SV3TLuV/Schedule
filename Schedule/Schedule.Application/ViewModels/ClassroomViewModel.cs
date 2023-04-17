using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class ClassroomViewModel : IMapWith<Classroom>
{
    public int Id { get; set; }

    public string Cabinet { get; set; } = null!;

    public ICollection<ClassroomTypeViewModel> Types { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Classroom, ClassroomViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(classroom => classroom.ClassroomId))
            .ForMember(viewModel => viewModel.Types, expression =>
                expression.MapFrom(classroom => classroom.ClassroomTypes))
            .ReverseMap();
    }
}
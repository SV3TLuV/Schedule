using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class TeacherViewModel : IMapWith<Teacher>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string MiddleName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool IsDeleted { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, TeacherViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(teacher => teacher.TeacherId));

        profile.CreateMap<TeacherViewModel, Teacher>()
            .ForMember(teacher => teacher.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Id));
    }
}
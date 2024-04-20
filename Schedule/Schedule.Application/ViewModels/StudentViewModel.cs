using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class StudentViewModel : IMapWith<Student>
{
    public int Id { get; set; }
    
    public GroupViewModel Group { get; set; } = null!;

    public string Login { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string Surname { get; set; } = null!;

    public string? MiddleName { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<Student, StudentViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(student => student.StudentId));

        profile.CreateMap<StudentViewModel, Student>()
            .ForMember(student => student.StudentId, expression =>
                expression.MapFrom(viewModel => viewModel.Id));
    }
}
using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class StudentViewModel : IMapWith<Student>
{
    public int Id { get; set; }
    
    public GroupViewModel Group { get; set; } = null!;
    
    public AccountViewModel Account { get; set; } = null!;

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
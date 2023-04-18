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

    public ICollection<GroupViewModel> Groups { get; set; } = null!;

    public ICollection<DisciplineViewModel> Disciplines { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Teacher, TeacherViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(teacher => teacher.TeacherId))
            .ForMember(viewModel => viewModel.Groups, expression =>
                expression.MapFrom(teacher => teacher.TeacherGroups
                    .Select(tg => tg.Group)))
            .ForMember(viewModel => viewModel.Disciplines, expression =>
                expression.MapFrom(teacher => teacher.TeacherDisciplines
                    .Select(td => td.Discipline)));
        
        profile.CreateMap<TeacherViewModel, Teacher>()
            .ForMember(teacher => teacher.TeacherId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(teacher => teacher.TeacherGroups, expression =>
                expression.MapFrom(viewModel => viewModel.Groups
                    .Select(group => new TeacherGroup
                    {
                        GroupId = group.Id,
                        TeacherId = viewModel.Id
                    })))
            .ForMember(teacher => teacher.TeacherDisciplines, expression =>
                expression.MapFrom(viewModel => viewModel.Disciplines
                    .Select(discipline => new TeacherDiscipline
                    {
                        DisciplineId = discipline.Id,
                        TeacherId = viewModel.Id
                    })));
    }
}
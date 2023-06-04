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
                expression.MapFrom(classroom =>
                    classroom.ClassroomClassroomTypes.Select(classroomClassroomType =>
                        classroomClassroomType.ClassroomType)));

        profile.CreateMap<ClassroomViewModel, Classroom>()
            .ForMember(classroom => classroom.ClassroomId, expression =>
                expression.MapFrom(viewModel => viewModel.Id))
            .ForMember(classroom => classroom.ClassroomClassroomTypes, expression =>
                expression.MapFrom(viewModel => viewModel.Types
                    .Select(type =>
                        new ClassroomClassroomType
                        {
                            ClassroomId = viewModel.Id,
                            ClassroomTypeId = type.Id
                        })));
    }
}
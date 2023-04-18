using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class ClassroomTypeViewModel : IMapWith<ClassroomType>
{
    public int Id { get; set; }

    public required string Name { get; set; }

    public bool IsDeleted { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<ClassroomType, ClassroomTypeViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(classroomType => classroomType.ClassroomTypeId))
            .ReverseMap();
    }
}
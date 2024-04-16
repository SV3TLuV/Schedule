using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public sealed class PermissionViewModel : IMapWith<Permission>
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public void Map(Profile profile)
    {
        profile.CreateMap<Permission, PermissionViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(weekType => weekType.PermissionId))
            .ReverseMap();
    }
}
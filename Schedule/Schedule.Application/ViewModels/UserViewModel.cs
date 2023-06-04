using AutoMapper;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.ViewModels;

public class UserViewModel : IMapWith<User>
{
    public int Id { get; set; }

    public required string Login { get; set; }

    public required RoleViewModel Role { get; set; }

    public void Map(Profile profile)
    {
        profile.CreateMap<User, UserViewModel>()
            .ForMember(viewModel => viewModel.Id, expression =>
                expression.MapFrom(user => user.UserId))
            .ReverseMap();
    }
}
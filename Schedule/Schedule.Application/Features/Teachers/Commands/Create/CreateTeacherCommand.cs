using AutoMapper;
using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Teachers.Commands.Create;

public sealed class CreateTeacherCommand : IRequest<int>, IMapWith<Teacher>
{
    public string Name { get; set; } = null!;
    public string Surname { get; set; } = null!;
    public string MiddleName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public ICollection<int> DisciplineIds { get; set; } = null!;
    public ICollection<int> GroupIds { get; set; } = null!;

    public void Map(Profile profile)
    {
    }
}
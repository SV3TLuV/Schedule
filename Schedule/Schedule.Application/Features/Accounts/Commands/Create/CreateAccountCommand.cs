using MediatR;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;

namespace Schedule.Application.Features.Accounts.Commands.Create;

public sealed class CreateAccountCommand : IRequest<int>, IMapWith<Account>
{
    public required string Login { get; set; }
    public required string Name { get; set; }
    public required string Surname { get; set; }
    public string? MiddleName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public int? GroupId { get; set; }
    public int RoleId { get; set; }
}
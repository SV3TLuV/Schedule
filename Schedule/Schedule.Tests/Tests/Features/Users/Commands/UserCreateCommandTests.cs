using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Users.Commands.Create;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests.Features.Users.Commands;

public sealed class UserCreateCommandTests
{
    [Fact]
    public async Task Test_CreateUser()
    {
        //Arrange
        var context = TestContainer.Resolve<IScheduleDbContext>();
        var mapper = TestContainer.Resolve<IMapper>();
        var command = new CreateUserCommand
        {
            Login = "TestUser",
            Password = "TestUser",
            RoleId = 1
        };

        //Act
        var handler = new CreateUserCommandHandler(context, mapper);
        var id = await handler.Handle(command, default);

        //Assert
        var user = await context.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                e.UserId == id &&
                e.Login == command.Login &&
                e.RoleId == command.RoleId);

        Assert.NotNull(user);
    }
}
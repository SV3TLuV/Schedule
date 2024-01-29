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
        //Получение контекста базы данных из DI контейнера
        var context = TestContainer.Resolve<IScheduleDbContext>();

        //Получение маппера из DI контейнера
        var mapper = TestContainer.Resolve<IMapper>();

        //Создание объекта команды добавления пользователя
        var command = new CreateUserCommand
        {
            Login = "TestUser",
            Password = "TestUser",
            RoleId = 1
        };

        //Act
        //Создание объекта обработчика команды добавления пользователя
        var handler = new CreateUserCommandHandler(context, mapper);

        //Вызов метода Handle для обработки команды
        var id = await handler.Handle(command, default);

        //Assert
        //Поиск пользователя по id, логину, и роли
        var user = await context.Set<User>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                e.UserId == id &&
                e.Login == command.Login &&
                e.RoleId == command.RoleId);

        //Проверка существования добавленного пользователя
        Assert.NotNull(user);
    }
}
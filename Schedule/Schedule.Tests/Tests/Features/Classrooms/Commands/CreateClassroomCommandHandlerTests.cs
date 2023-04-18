using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Classrooms.Commands.Create;
using Schedule.Core.Models;
using Schedule.Persistence.Context;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests.Features.Classrooms.Commands;

public class CreateClassroomCommandHandlerTests
{
    [Fact]
    public async Task Test_Handle_AddsClassroom()
    {
        //Arrange
        var context = TestContainer.Resolve<ScheduleDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<CreateClassroomCommand, int>>();
        var command = new CreateClassroomCommand
        {
            Cabinet = "0109",
            TypeIds = new[] { 1, 2 }
        };
        var expected = new Classroom
        {
            ClassroomId = 1,
            Cabinet = "0109",
            IsDeleted = false,
            ClassroomClassroomTypes = new List<ClassroomClassroomType>
            {
                new() { ClassroomId = 1, ClassroomTypeId = 1 },
                new() { ClassroomId = 1, ClassroomTypeId = 2 },
            }
        };

        //Act
        await context.Database.EnsureCreatedAsync();
        await context.ClassroomTypes.AddRangeAsync(new[]
        {
            new ClassroomType
            {
                ClassroomTypeId = 1,
                Name = "Лекционный",
            },
            new ClassroomType
            {
                ClassroomTypeId = 2,
                Name = "Компьютерный",
            },
        });
        await context.SaveChangesAsync();
        await handler.Handle(command, default);
        var actual = await context.Classrooms
            .Include(e => e.ClassroomClassroomTypes)
            .AsNoTracking()
            .FirstOrDefaultAsync();
        await context.Database.EnsureDeletedAsync();

        //Assert
        Assert.Equal(expected, actual);
    }
}
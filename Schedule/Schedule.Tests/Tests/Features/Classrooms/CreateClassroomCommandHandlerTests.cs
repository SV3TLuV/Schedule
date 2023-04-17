using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Classrooms.Commands.Create;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Context;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests.Features.Classrooms;

public sealed class CreateClassroomCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(HandleCorrectData))]
    public async Task Test_Handle_AddsClassroom(
        CreateClassroomCommand command, Classroom expected)
    {
        // Arrange
        var context = TestContainer.Resolve<ScheduleDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<CreateClassroomCommand, int>>();

        // Act
        await handler.Handle(command, default);
        var actual = await context.Classrooms
            .AsNoTrackingWithIdentityResolution()
            .Include(e => e.ClassroomTypes)
            .FirstOrDefaultAsync();
        
        // Assert
        Assert.Equal(expected, actual);
    }

    public static IEnumerable<object[]> HandleCorrectData =>
        new[]
        {
            new object[]
            {
                new CreateClassroomCommand
                {
                    Cabinet = "0109",
                    TypeIds = new [] { 1, 2 }
                },
                new Classroom
                {
                    Cabinet = "0109",
                    ClassroomTypes = new []
                    {
                        new ClassroomType
                        {
                            ClassroomTypeId = 1,
                            Name = "Лекционный"
                        },
                        new ClassroomType
                        {
                            ClassroomTypeId = 2,
                            Name = "Компьютерный"
                        }
                    }
                }
            }
        };
}
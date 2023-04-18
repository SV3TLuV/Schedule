using AutoBogus;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Classrooms.Commands.Create;
using Schedule.Core.Models;
using Schedule.Persistence.Context;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests.Features.Classrooms.Commands;

public class CreateClassroomCommandHandlerTests
{
    [Theory]
    [MemberData(nameof(Data))]
    public async Task Test_Handle_AddsClassroom(
        CreateClassroomCommand command, ClassroomType[] types)
    {
        //Arrange
        var context = TestContainer.Resolve<ScheduleDbContext>();
        var handler = TestContainer.Resolve<IRequestHandler<CreateClassroomCommand, int>>();

        //Act
        await context.Database.EnsureCreatedAsync();
        await context.ClassroomTypes.AddRangeAsync(types);
        await context.SaveChangesAsync();

        command.TypeIds = context.ClassroomTypes
            .AsNoTrackingWithIdentityResolution()
            .Select(e => e.ClassroomTypeId)
            .ToArray();

        await handler.Handle(command, default);

        var classroomsDbo = await context
            .Set<Classroom>()
            .ToArrayAsync();
        
        var classroomClassroomTypesDbo = await context
            .Set<ClassroomClassroomType>()
            .ToArrayAsync();
        await context.Database.EnsureDeletedAsync();
        
        //Assert
        Assert.Single(classroomsDbo);
        Assert.Equal(command.Cabinet, classroomsDbo.Single().Cabinet);
        Assert.Equal(types.Length, classroomClassroomTypesDbo.Length);
    }

    public static IEnumerable<object[]> Data =>
        Enumerable.Range(1, 10)
            .Select(count => new object[]
            {
                new AutoFaker<CreateClassroomCommand>()
                    .Ignore(e => e.TypeIds)
                    .RuleFor(e => e.Cabinet, f => f.Random
                        .String2(1, 10, "0123456789"))
                    .Generate(),
                new AutoFaker<ClassroomType>()
                    .Ignore(e => e.ClassroomClassroomTypes)
                    .RuleFor(e => e.Name, f => f.Random
                        .String2(1, 50))
                    .Generate(count)
            });
}

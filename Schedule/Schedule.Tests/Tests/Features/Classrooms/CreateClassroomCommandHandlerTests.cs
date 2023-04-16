using AutoMapper;
using MediatR;
using Schedule.Application.Features.Classrooms.Commands.Create;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Context;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests.Features.Classrooms;

public sealed class CreateClassroomCommandHandlerTests
{
    [Fact]
    public async Task Test_Handle_AddsClassroom()
    {
        // Arrange
        var context = TestContainer.Resolve<IScheduleDbContext>();
        var mapper = TestContainer.Resolve<IMapper>();
        var handler = TestContainer.Resolve<IRequestHandler<CreateClassroomCommand, int>>();

        // Act

        // Assert
        Assert.True(false);
    }
}
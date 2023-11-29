using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Features.Specialities.Commands.Create;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Tests.Common;

namespace Schedule.Tests.Tests.Features.Specialities.Commands;

public sealed class SpecialityCreateCommandTests
{
    [Fact]
    public async Task Test_CreateSpeciality()
    {
        //Arrange
        var context = TestContainer.Resolve<IScheduleDbContext>();
        var mapper = TestContainer.Resolve<IMapper>();
        var command = new CreateSpecialityCommand
        {
            Code = "09.02.07",
            Name = "ИСПП",
            MaxTermId = 8
        };

        //Act
        var handler = new CreateSpecialityCommandHandler(context, mapper);
        var id = await handler.Handle(command, default);

        //Assert
        var user = await context.Set<Speciality>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                e.SpecialityId == id &&
                e.Code == command.Code &&
                e.Name == command.Name &&
                e.MaxTermId == command.MaxTermId);

        Assert.NotNull(user);
    }
}
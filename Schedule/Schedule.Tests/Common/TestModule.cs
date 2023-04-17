using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Jobs;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Core.Models;
using Schedule.Persistence.Context;

namespace Schedule.Tests.Common;

internal class TestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ScheduleDbContext>()
            .As<IScheduleDbContext>()
            .AsSelf()
            .WithParameter("options",
                new DbContextOptionsBuilder<ScheduleDbContext>()
                .UseInMemoryDatabase("Schedule.Tests")
                .Options)
            .InstancePerDependency();

        builder
            .Register(_ =>
            {
                var options = new DbContextOptionsBuilder<ScheduleDbContext>()
                    .UseInMemoryDatabase("Schedule.Tests")
                    .Options;
                var context = new ScheduleDbContext(options);

                context.Database.EnsureCreated();
                
                context.ClassroomTypes.AddRange(new []
                {
                    new ClassroomType
                    {
                        ClassroomTypeId = 1,
                        Name = "Лекционный",
                    },
                    new ClassroomType
                    {
                        ClassroomTypeId = 2,
                        Name = "Компьютергый",
                    },
                    new ClassroomType
                    {
                        ClassroomTypeId = 3,
                        Name = "С проектором",
                    },
                });
                
                return context;
            })
            .As<IScheduleDbContext>()
            .AsSelf()
            .InstancePerDependency();

        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>();

        builder.RegisterType<GenerateDatesJob>()
            .AsSelf();
    }
}
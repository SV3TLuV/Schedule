using Autofac;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Jobs;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Context;

namespace Schedule.Tests.Common;

internal class TestModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ScheduleDbContext>()
            .As<IScheduleDbContext>()
            .WithParameter("options",
                new DbContextOptionsBuilder<ScheduleDbContext>()
                .UseInMemoryDatabase("Schedule.Tests")
                .Options)
            .InstancePerLifetimeScope();
        
        builder.RegisterMediatR(MediatRConfigurationBuilder
            .Create(ThisAssembly)
            .Build());
        
        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>();

        builder.RegisterType<GenerateDatesJob>()
            .AsSelf();
    }
}
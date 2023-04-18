using Autofac;
using Autofac.Extensions.DependencyInjection;
using MediatR;
using MediatR.Extensions.Autofac.DependencyInjection;
using MediatR.Extensions.Autofac.DependencyInjection.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
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
        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>();

        builder.RegisterType<GenerateDatesJob>()
            .AsSelf();

        var services = new ServiceCollection();

        services.AddDbContext<IScheduleDbContext, ScheduleDbContext>(options => 
            options.UseInMemoryDatabase(Guid.NewGuid().ToString()), ServiceLifetime.Transient);

        builder.Populate(services);
    }
}
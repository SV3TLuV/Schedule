using Autofac;
using Microsoft.EntityFrameworkCore;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Context;

namespace Schedule.Api.Modules;

public sealed class ApiModule : Module
{
    private readonly IConfiguration _configuration;

    public ApiModule(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<ScheduleDbContext>()
            .As<IScheduleDbContext>()
            .WithParameter("options", 
                new DbContextOptionsBuilder<ScheduleDbContext>()
                    .UseSqlServer(_configuration.GetConnectionString("ScheduleWin"))
                    .Options)
            .InstancePerDependency();

        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>()
            .InstancePerDependency();
    }
}
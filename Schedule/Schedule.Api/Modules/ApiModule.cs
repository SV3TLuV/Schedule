using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Api.Common;
using Schedule.Application.Common.Behaviors;
using Schedule.Application.Jobs;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Context;

namespace Schedule.Api.Modules;

public sealed class ApiModule : Module
{
    protected override void Load(ContainerBuilder builder)
    {
        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>()
            .InstancePerDependency();

        var services = new ServiceCollection();

        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddFluentValidationAutoValidation()
            .AddDbContext<IScheduleDbContext, ScheduleDbContext>(options =>
                options.UseSqlServer("Name=ScheduleWin"))
            .AddCors(options => options.AddPolicy(Constants.CorsName, policy =>
            {
                policy
                    .WithMethods(
                        HttpMethods.Get,
                        HttpMethods.Post,
                        HttpMethods.Put,
                        HttpMethods.Delete)
                    .AllowAnyHeader()
                    .AllowCredentials()
                    .SetIsOriginAllowed(_ => true);
            }))
            .AddQuartzServer(options =>
            {
                options.WaitForJobsToComplete = true;
            })
            .AddQuartz(configuration =>
            {
                configuration.SchedulerId = "Schedule-Scheduler-Id";
                configuration.SchedulerName = "Schedule-Scheduler-Name";
                configuration.UseMicrosoftDependencyInjectionJobFactory();
                configuration.UseInMemoryStore();
                configuration.UseDefaultThreadPool(pool =>
                {
                    pool.MaxConcurrency = 10;
                });

                configuration.AddJob<GenerateDatesJob>(options =>
                    options.WithIdentity("GenerateDatesJob"));

                configuration.AddTrigger(configure =>
                    configure.ForJob("GenerateDatesJob")
                        .WithIdentity("GenerateDatesJobTrigger")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInHours(6)
                            .RepeatForever()));
                
                configuration.AddJob<TransferGroupsJob>(options =>
                    options.WithIdentity("TransferGroupsJob"));
                
                configuration.AddTrigger(configure =>
                    configure.ForJob("TransferGroupsJob")
                        .WithIdentity("TransferGroupsJobTrigger")
                        .WithSimpleSchedule(x => x
                            .WithIntervalInHours(6)
                            .RepeatForever()));
            });
        
        builder.Populate(services);
    }
}
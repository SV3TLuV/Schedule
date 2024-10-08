﻿using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Quartz;
using Quartz.AspNetCore;
using Schedule.Api.Common;
using Schedule.Api.Common.Behavior;
using Schedule.Application.Common.Behaviors;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Jobs;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Common.Interfaces;
using Schedule.Persistence.Context;
using Schedule.Persistence.Initializers;

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
        builder.RegisterType<DateInfoService>()
            .As<IDateInfoService>()
            .InstancePerDependency();

        builder.RegisterType<TokenService>()
            .As<ITokenService>()
            .SingleInstance();

        var services = new ServiceCollection();

        services.AddSignalR();

        services
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
                    .SetIsOriginAllowed(_ => true)
                    .WithExposedHeaders("content-disposition");
            }))
            .ConfigureApplicationCookie(options => { options.Cookie.SameSite = SameSiteMode.None; })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = _configuration["Jwt:Issuer"],
                    ValidAudience = _configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]))
                };
            });

        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(NotificationBehavior<,>))
            .AddTransient<IDbInitializer, DatabaseInitializer>()
            .AddFluentValidationAutoValidation()
            .AddDbContext<IScheduleDbContext, ScheduleDbContext>(options =>
                options.UseSqlServer($"Name={Constants.ConnectionStringName}"))
            .AddQuartzServer(options => { options.WaitForJobsToComplete = true; })
            .AddQuartz(configuration =>
            {
                configuration.SchedulerId = "Schedule-Scheduler-Id";
                configuration.SchedulerName = "Schedule-Scheduler-Name";
                configuration.UseMicrosoftDependencyInjectionJobFactory();
                configuration.UseInMemoryStore();
                configuration.UseDefaultThreadPool(pool => { pool.MaxConcurrency = 10; });

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
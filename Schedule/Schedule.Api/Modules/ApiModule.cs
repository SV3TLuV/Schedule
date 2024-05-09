using System.Text;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Schedule.Api.Common;
using Schedule.Application.Common.Behaviors;
using Schedule.Application.Common.Interfaces;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Context;

namespace Schedule.Api.Modules;

public sealed class ApiModule(IConfiguration configuration) : Module
{
    protected override void Load(ContainerBuilder builder)
    {
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
                    ClockSkew = TimeSpan.FromSeconds(0),
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = configuration["Jwt:Issuer"],
                    ValidAudience = configuration["Jwt:Audience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]))
                };
            });

        services
            .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
            .AddFluentValidationAutoValidation()
            .AddDbContext<IScheduleDbContext, ScheduleDbContext>(options =>
                options.UseNpgsql($"Name={Constants.PostgresqlConnectionString}"));
            // .AddQuartzServer(options => { options.WaitForJobsToComplete = true; })
            // .AddQuartz(configuration =>
            // {
            //     configuration.SchedulerId = "Schedule-Scheduler-Id";
            //     configuration.SchedulerName = "Schedule-Scheduler-Name";
            //     configuration.UseMicrosoftDependencyInjectionJobFactory();
            //     configuration.UseInMemoryStore();
            //     configuration.UseDefaultThreadPool(pool => { pool.MaxConcurrency = 10; });
            //
            //     configuration.AddJob<TransferGroupsJob>(options =>
            //         options.WithIdentity("TransferGroupsJob"));
            //
            //     configuration.AddTrigger(configure =>
            //         configure.ForJob("TransferGroupsJob")
            //             .WithIdentity("TransferGroupsJobTrigger")
            //             .WithSimpleSchedule(x => x
            //                 .WithIntervalInHours(6)
            //                 .RepeatForever()));
            // })

            services
                .Configure<RequestLocalizationOptions>(option =>
                {
                    option.DefaultRequestCulture = new RequestCulture(culture: "ru-RU", uiCulture: "ru-RU");
                });

        builder.Populate(services);
    }
}
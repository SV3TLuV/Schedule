using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Schedule.Api.Common;
using Schedule.Application.Common.Behaviors;
using Schedule.Application.Common.Mappings;
using Schedule.Application.Services;
using Schedule.Core.Common.Interfaces;
using Schedule.Persistence.Context;

var builder = WebApplication.CreateBuilder(args);
ConfigureServices(builder.Services);
var app = builder.Build();
ConfigureApp(app);
app.Run();


void ConfigureServices(IServiceCollection services)
{
    var assembly = Assembly.GetAssembly(typeof(AssemblyMappingProfile))!;
    
    services
        .AddTransient<IDateInfoService, DateInfoService>()
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
        .AddValidatorsFromAssembly(assembly)
        .AddFluentValidationAutoValidation()
        .AddAutoMapper(options => options.AddProfile(new AssemblyMappingProfile(assembly)))
        .AddMediatR(options => options.RegisterServicesFromAssembly(assembly))
        .AddDbContext<IScheduleDbContext, ScheduleDbContext>(options =>
            options.UseSqlServer("Name=Schedule"))
        .AddCors(options => options.AddPolicy(Variables.CorsName, policy =>
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
            configuration.UseSimpleTypeLoader();
            configuration.UseInMemoryStore();
            configuration.UseDefaultThreadPool(pool =>
            {
                pool.MaxConcurrency = 10;
            });
            
            //TODO: Need JobFactory for add from DI
            /*configuration.AddJob<GenerateDatesJob>(options =>
                options.WithIdentity("GenerateDatesJob"));
            configuration.AddTrigger(configure =>
                configure.ForJob("GenerateDatesJob")
                    .WithIdentity("GenerateDatesJobTrigger")
                    .WithSimpleSchedule(x => x
                        .WithIntervalInHours(6)
                        .RepeatForever()));*/
        })
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddControllers();
}

void ConfigureApp(WebApplication webApp)
{
    webApp
        .UseSwagger()
        .UseSwaggerUI()
        .UseCors(Variables.CorsName)
        .UseHttpsRedirection()
        .UseAuthorization();
    webApp.MapControllers();
}
using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation.AspNetCore;
using MediatR;
using Quartz;
using Schedule.Api.Common;
using Schedule.Api.Modules;
using Schedule.Application.Common.Behaviors;
using Schedule.Application.Modules;

var applicationBuilder = WebApplication.CreateBuilder(args);
applicationBuilder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory(configuration =>
    {
        configuration.RegisterModule(new ApiModule(applicationBuilder.Configuration));
        configuration.RegisterModule<ApplicationModule>();
    }));
ConfigureServices(applicationBuilder.Services);
var app = applicationBuilder.Build();
ConfigureApp(app);
app.Run();


void ConfigureServices(IServiceCollection services)
{
    services
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
        .AddFluentValidationAutoValidation()
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
        .AddControllers()
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        });
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
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using MediatR;
using Schedule.Api.Common;
using Schedule.Application.Common.Behaviors;
using Schedule.Application.Common.Mappings;
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
        .AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>))
        .AddValidatorsFromAssembly(assembly)
        .AddFluentValidationAutoValidation()
        .AddAutoMapper(options => options.AddProfile(new AssemblyMappingProfile(assembly)))
        .AddMediatR(options => options.RegisterServicesFromAssembly(assembly))
        .AddDbContext<ScheduleDbContext>()
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
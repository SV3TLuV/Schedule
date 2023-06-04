using System.Text.Json.Serialization;
using System.Threading.RateLimiting;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.AspNetCore.RateLimiting;
using Schedule.Api.Common;
using Schedule.Api.Middleware.CustomException;
using Schedule.Api.Modules;
using Schedule.Application.LoggerPolicies;
using Schedule.Application.Modules;
using Serilog;
using Serilog.Sinks.MSSqlServer;

try
{
    var applicationBuilder = WebApplication.CreateBuilder(args);

    ConfigureLogger(applicationBuilder.Configuration);

    applicationBuilder.Host
        .UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
        {
            var configuration = applicationBuilder.Configuration;

            builder.RegisterModule(new ApiModule(configuration));
            builder.RegisterModule(new ApplicationModule(configuration));
        }))
        .ConfigureServices(services =>
        {
            services
                .AddEndpointsApiExplorer()
                .AddSwaggerGen()
                .AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
                });
        });

    var app = applicationBuilder.Build();

    ConfigureApp(app);

    app.Run();
}
catch (Exception e)
{
    Log.Fatal(e, "Fatal error on start app");
}
finally
{
    await Log.CloseAndFlushAsync();
}


void ConfigureApp(WebApplication webApp)
{
    webApp
        .UseCustomExceptionHandler()
        .UseSwagger()
        .UseSwaggerUI()
        .UseCors(Constants.CorsName)
        .UseHttpsRedirection()
        .UseAuthentication()
        .UseAuthorization();
    webApp.MapControllers();
}

void ConfigureLogger(IConfiguration configuration)
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.MSSqlServer(
            configuration.GetConnectionString("ScheduleWin"),
            new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            })
        .Destructure.With<MaskingDestructuringPolicy>()
        .CreateLogger();
}
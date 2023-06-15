using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Schedule.Api.Common;
using Schedule.Api.Hubs;
using Schedule.Api.Middleware.CustomException;
using Schedule.Api.Modules;
using Schedule.Application.LoggerPolicies;
using Schedule.Application.Modules;
using Schedule.Persistence.Common.Interfaces;
using Serilog;
using Serilog.Sinks.MSSqlServer;
using StackExchange.Profiling.Internal;

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
            /*
            services
                .AddMemoryCache()
                .AddMiniProfiler(options => options.RouteBasePath = "/profiler")
                .AddEntityFramework();
                */
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

    // var initializer = app.Services.GetRequiredService<IDbInitializer>();
    // await initializer.InitializeAsync();
    
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
        .UseHttpsRedirection()
        .UseCors(Constants.CorsName)
        .UseAuthentication()
        .UseAuthorization();
    webApp.MapControllers();
    webApp.MapHub<NotificationHub>("/hub/notification");
    //webApp.UseMiniProfiler();
}

void ConfigureLogger(IConfiguration configuration)
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.MSSqlServer(
            configuration.GetConnectionString(Constants.ConnectionStringName),
            new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true
            })
        .Destructure.With<MaskingDestructuringPolicy>()
        .CreateLogger();
}
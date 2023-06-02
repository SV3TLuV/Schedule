using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Schedule.Api.Common;
using Schedule.Api.Modules;
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
        
            builder.RegisterModule<ApiModule>();
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
        .UseSwagger()
        .UseSwaggerUI()
        .UseCors(Constants.CorsName)
        .UseHttpsRedirection()
        .UseAuthorization();
    webApp.MapControllers();
}

void ConfigureLogger(IConfiguration configuration)
{
    Log.Logger = new LoggerConfiguration()
        .WriteTo.MSSqlServer(
            configuration.GetConnectionString("ScheduleWin"),
            sinkOptions: new MSSqlServerSinkOptions
            {
                TableName = "Logs",
                AutoCreateSqlTable = true,
            })
        .CreateLogger();
}
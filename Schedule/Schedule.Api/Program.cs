using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Schedule.Api.Common;
using Schedule.Api.Hubs;
using Schedule.Api.Middleware.CustomException;
using Schedule.Api.Modules;
using Schedule.Application.Modules;
using Schedule.Persistence.Common.Interfaces;

var applicationBuilder = WebApplication.CreateBuilder(args);

applicationBuilder.Host
    .UseServiceProviderFactory(new AutofacServiceProviderFactory(builder =>
    {
        var configuration = applicationBuilder.Configuration;
            
        builder.RegisterModule(new ApiModule(configuration));
        builder.RegisterModule<ApplicationModule>();
    }))
    .ConfigureServices(services =>
    {
        // services
        //     .AddMemoryCache()
        //     .AddMiniProfiler(options => options.RouteBasePath = "/profiler")
        //     .AddEntityFramework();
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

var initializer = app.Services.GetRequiredService<IDbInitializer>();
await initializer.InitializeAsync();
    
ConfigureApp(app);

app.Run();

void ConfigureApp(WebApplication webApp)
{
    webApp
        .UseCustomExceptionHandler()
        .UseSwagger()
        .UseSwaggerUI();
    //webApp.UseMiniProfiler();
    webApp.UseRouting();
    webApp.UseCors(Constants.CorsName);
    webApp.UseAuthentication();
    webApp.UseAuthorization();
    webApp.MapControllers();
    webApp.MapHub<NotificationHub>("/hub/notification");
}
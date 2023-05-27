using System.Text.Json.Serialization;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Schedule.Api.Common;
using Schedule.Api.Modules;
using Schedule.Application.Modules;

var applicationBuilder = WebApplication.CreateBuilder(args);
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
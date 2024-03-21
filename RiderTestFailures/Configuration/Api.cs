using Carter;
using RiderTestFailures.ErrorHandler;

namespace RiderTestFailures.Configuration;

public static class Api
{
    public static IServiceCollection AddApiServices(this IServiceCollection services)
    {
        services.AddCarter();

        return services;
    }
    
    public static WebApplication ConfigureApi(this WebApplication app)
    {
        app.MapCarter();
        app.UseApiExceptionHandler();

        return app;
    }
}
using Microsoft.AspNetCore.Diagnostics;

namespace RiderTestFailures.ErrorHandler;

public static class ErrorHandlingExtensions
{
    public static IApplicationBuilder UseApiExceptionHandler(this IApplicationBuilder app) =>
        app.UseExceptionHandler(a => a.Run(async context =>
        {
            var exceptionHandlerPathFeature = context.Features.Get<IExceptionHandlerPathFeature>();
            if (exceptionHandlerPathFeature == null)
            {
                return;
            }

            if (context.RequestAborted.IsCancellationRequested && exceptionHandlerPathFeature.Error is TaskCanceledException)
            {
                context.Response.StatusCode = 424; // Failed dependency, because the server depends on the client. Tenuous, yes. 
                return;
            }
            
            var exception = exceptionHandlerPathFeature.Error;
            var logger = context.RequestServices.GetService<ILoggerFactory>()?.CreateLogger(nameof(ErrorHandlingExtensions));
            logger?.LogError(exception.ToString());
            
            await context.Response.WriteAsJsonAsync(new { error = "An unexpected error occurred on the server. Please try again later."}).ConfigureAwait(false);
        }));
}
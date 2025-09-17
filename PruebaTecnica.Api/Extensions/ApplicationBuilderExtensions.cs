
using PruebaTecnica.Api.Middleware;

namespace PruebaTecnica.Api.Extensions;

public static class ApplicationBuilderExtensions
{

    public static void UseCustomExceptionHandler(this IApplicationBuilder app)
    {
        app.UseMiddleware<ExceptionHandlingMiddleware>();
    }

    public static IApplicationBuilder UseRequestContextLogging(
        this IApplicationBuilder app
    )
    {
        app.UseMiddleware<RequestContextLoggingMiddleware>();

        return app;
    }
}
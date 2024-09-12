using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

public static class CustomMiddlewareExtensions
{
    public static IApplicationBuilder UseCustomExceptionHandling(this IApplicationBuilder app)
    {
        // Global Exception Handling Middleware
        app.UseExceptionHandler(errorApp =>
        {
            errorApp.Run(async context =>
            {
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("An error occurred.");
            });
        });

        return app;
    }

    public static IApplicationBuilder UseCustomUnauthorizedHandling(this IApplicationBuilder app)
    {
        // Unauthorized Access Handling Middleware
        app.Use(async (context, next) =>
        {
            await next();
            if (context.Response.StatusCode == StatusCodes.Status401Unauthorized)
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("You are not authorized.");
            }
        });

        return app;
    }

    public static IApplicationBuilder UseCustomNotFoundHandling(this IApplicationBuilder app)
    {
        // 404 Error Handling Middleware
        app.Use(async (context, next) =>
        {
            await next();
            if (context.Response.StatusCode == StatusCodes.Status404NotFound)
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("This route doesn't exist.");
            }
        });

        return app;
    }

    public static IApplicationBuilder UseCustomMethodNotAllowedHandling(this IApplicationBuilder app)
    {
        // 405 Method Not Allowed Handling Middleware
        app.Use(async (context, next) =>
        {
            await next();
            if (context.Response.StatusCode == StatusCodes.Status405MethodNotAllowed)
            {
                context.Response.ContentType = "text/plain";
                await context.Response.WriteAsync("This method doesn't exist.");
            }
        });

        return app;
    }
}

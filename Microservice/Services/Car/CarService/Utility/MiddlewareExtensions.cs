using Microsoft.AspNetCore.Builder;

namespace CarService.Utility
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
               => app.UseMiddleware<ApiExceptionHandlingMiddleware>();
    }
}

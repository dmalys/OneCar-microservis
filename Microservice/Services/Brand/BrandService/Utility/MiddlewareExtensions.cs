using Microsoft.AspNetCore.Builder;

namespace BrandService.Utility
{
    public static class MiddlewareExtensions
    {
        public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
               => app.UseMiddleware<ApiExceptionHandlingMiddleware>();
    }
}

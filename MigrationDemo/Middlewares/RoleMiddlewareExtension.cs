using Microsoft.AspNetCore.Builder;

namespace MigrationDemo.Middlewares
{
    public static class RoleMiddlewareExtension 
    {
        public static IApplicationBuilder UseRoleMiddleware(this IApplicationBuilder builder) 
        {
            return builder.UseMiddleware<RoleMiddleware>();
        }
    }
}

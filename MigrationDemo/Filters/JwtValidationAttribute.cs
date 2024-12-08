using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using MigrationDemo.Services;

namespace MigrationDemo.Filters
{
    public class JwtValidationAttribute:Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var authorizationHeader = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault();

            if (string.IsNullOrEmpty(authorizationHeader))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            // Extract the token if it uses the "Bearer" scheme
            var token = authorizationHeader.StartsWith("Bearer ") ? authorizationHeader.Substring(7) : authorizationHeader;

            Console.WriteLine(token);

            var jwtService = context.HttpContext.RequestServices.GetRequiredService<JwtService>();

            if (string.IsNullOrEmpty(token) || !jwtService.ValidateToken(token))
            {
                context.Result = new UnauthorizedResult();
                return;
            }
        }
    }
}

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Security.Claims;

namespace MigrationDemo.Filters
{
    public class RoleAuthorizeAttribute: Attribute, IAuthorizationFilter
    {
        private readonly string role;

        public RoleAuthorizeAttribute(string role)
        {
            this.role = role;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var r = context.HttpContext.User.FindFirst(ClaimTypes.Role)?.Value;

            if (r == null || r != role)
            {
                context.Result = new UnauthorizedResult();
            }
        }
    }
}

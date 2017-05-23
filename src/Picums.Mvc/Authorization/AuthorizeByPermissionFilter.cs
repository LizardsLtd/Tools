using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Picums.Mvc.Authorization
{
    public sealed class AuthorizeByPermissionFilter : AuthorizeFilter
    {
        public AuthorizeByPermissionFilter() : base(GetPolicy())
        {
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            base.OnAuthorizationAsync(context);

            return Task.CompletedTask;
        }

        private static AuthorizationPolicy GetPolicy()
            => new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new PermissionRequirement())
                    .Build();
    }
}
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Picums.Mvc.UserAccess
{
    public sealed class AuthorizeByPermissionFilter : AuthorizeFilter
    {
        private readonly RedirectToActionResult redirectOnFailureAction;

        public AuthorizeByPermissionFilter(string controller, string action)
            : base(GetPolicy())
        {
            this.redirectOnFailureAction = new RedirectToActionResult(action, controller, new object[0]);
        }

        public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            base.OnAuthorizationAsync(context);

            if (this.IsUnAuthorizeAccess(context))
            {
                context.Result = this.redirectOnFailureAction;
            }

            return Task.CompletedTask;
        }

        private static AuthorizationPolicy GetPolicy()
            => new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                .AddRequirements(new PermissionRequirement())
                .Build();

        private bool IsUnAuthorizeAccess(AuthorizationFilterContext context)
            => context.HttpContext.User.Identity.IsAuthenticated
                || this.AnonymousAccessIsNotAllowed(context);

        private bool AnonymousAccessIsNotAllowed(AuthorizationFilterContext context)
            => !context.Filters.Any(x => x.GetType() == typeof(AllowAnonymousFilter));
    }
}
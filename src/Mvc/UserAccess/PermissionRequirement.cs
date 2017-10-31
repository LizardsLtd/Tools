using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Controllers;
using Picums.Mvc.UserAccess.Claims;
using MvcAuthorizationContext = Microsoft.AspNetCore.Mvc.Filters.AuthorizationFilterContext;

namespace Picums.Mvc.UserAccess
{
    internal sealed class PermissionRequirement : AuthorizationHandler<PermissionRequirement>, IAuthorizationRequirement
    {
        public override Task HandleAsync(AuthorizationHandlerContext context)
        {
            return base.HandleAsync(context);
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
            => Task.Run(() => ExecutePermissionEvaluation(context, requirement));

        private void ExecutePermissionEvaluation(
           AuthorizationHandlerContext context
           , PermissionRequirement requirement)
        {
            var resources = (MvcAuthorizationContext)context.Resource;
            var actionDescription = (ControllerActionDescriptor)resources.ActionDescriptor;
            var attributtes = actionDescription.MethodInfo.CustomAttributes;

            bool hasAllPermissions = CheckForPermissions(context, attributtes);

            if (hasAllPermissions)
            {
                context.Succeed(requirement);
            }
            else
            {
                context.Fail();
            }
        }

        private bool CheckForPermissions(AuthorizationHandlerContext context, IEnumerable<CustomAttributeData> attributtes)
            => attributtes
                .Where(x => x.AttributeType == typeof(RequirePermissionAttribute))
                .ToList()
                .Where(x => x.NamedArguments != null)
                .Where(x => x.NamedArguments.Any())
                .Select(x => x.NamedArguments[0].TypedValue.Value.ToString())
                .Select(x => new Permission(x))
                .All(x => context.User.HasClaim(ClaimIdentity.Permission, x.ToString()));
    }
}
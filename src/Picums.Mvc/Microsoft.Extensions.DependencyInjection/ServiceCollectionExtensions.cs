using Microsoft.AspNetCore.Identity;
using Picums.Data.Claims;
using Picums.Mvc.Authorization;

namespace Microsoft.Extensions.DependencyInjection
{
    //todo drop this file
    public static class IdentityServiceCollectionExtensions
    {
        public static IServiceCollection AddClaimsIdentity<TUser>(this IServiceCollection services)
            where TUser : class, IClaimsProvider, IUser
        {
            services.AddIdentity<TUser, string>();
            return services
                .AddScoped<IUserClaimsPrincipalFactory<TUser>, ClaimsPrincipalFactory<TUser>>();
        }
    }
}
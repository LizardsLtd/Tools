using Microsoft.AspNetCore.Identity;
using TheLizzards.DataParts.Contracts;
using TheLizzards.Mvc.Claims.Entities;

namespace Microsoft.Extensions.DependencyInjection
{
	//todo drop this file
	public static class IdentityServiceCollectionExtensions
	{
		public static IServiceCollection AddClaimsIdentity<TUser>(this IServiceCollection services)
			where TUser : class, IClaimsProvider, IIdProvider
		{
			services.AddIdentity<TUser, string>();
			return services
				.AddScoped<IUserClaimsPrincipalFactory<TUser>, ClaimsPrincipalFactory<TUser>>();
		}
	}
}
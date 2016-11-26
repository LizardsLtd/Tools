using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using TheLizzards.Mvc.Claims.Entities;
using TheLizzards.Mvc.FeatureSlices;
using TheLizzards.Mvc.Startup;

namespace TheLizzards.Mvc
{
	public static class DefaultsBootstrapper
	{
		public static IConfiguration UseFeatures(this IConfiguration startup)
			=> startup
				  .ForMvcOption()
					.AddControllerConvention<FeatureConvention>()
				.AddFeatureSlice();

		public static IConfiguration AddPermissionBasedAuthorization(this IConfiguration startup)
			=> startup
				.ForMvcOption()
				.AddMvcFilter(
					new AuthorizeFilter(
						new AuthorizationPolicyBuilder()
							.RequireAuthenticatedUser()
							.AddRequirements(new PermissionRequirement())
							.Build()));
	}
}
using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using TheLizzards.DataParts.Contracts;

namespace TheLizzards.Mvc.Startup
{
	public static class AuthenticationBootstrapper
	{
		public static IConfiguration AddAuthentication<TUser, TUserStore>(this IConfiguration startup)
			where TUser : class, IClaimsProvider, IIdProvider
			where TUserStore : class, IUserStore<TUser>
		{
			startup.AddConfiguration((app, e, lf) => app.UseIdentity());
			startup.AddServices(services
				=> services
					.AddClaimsIdentity<TUser>()
					.AddScoped<IUserStore<TUser>, TUserStore>());
			return startup;
		}

		public static IConfiguration ConfigureIdentityOptions(this IConfiguration startup
			, string loginPath = "/Login"
			, string logoutPath = "/Logout"
			, string accessDeniedPath = "/Error/RestrictedAccess"
			, int requiredLenght = 8
			, bool slidingExpiration = true
			, bool autmationChallange = true)
		{
			startup.ConfigureOption<IdentityOptions>(
				SetIdentityOptions(
					loginPath
					, logoutPath
					, accessDeniedPath
					, requiredLenght
					, slidingExpiration
					, autmationChallange));
			return startup;
		}

		private static Action<IdentityOptions> SetIdentityOptions(
		   string loginPath = "/Login"
		   , string logoutPath = "/Logout"
		   , string accessDeniedPath = "/Error/RestrictedAccess"
		   , int requiredLenght = 8
		   , bool slidingExpiration = true
		   , bool autmationChallange = true)
			=> new Action<IdentityOptions>(options =>
			   {
				   options.Password.RequiredLength = requiredLenght;
				   options.Cookies.ApplicationCookie.LoginPath = new PathString(loginPath);
				   options.Cookies.ApplicationCookie.LogoutPath = new PathString(logoutPath);
				   options.Cookies.ApplicationCookie.AccessDeniedPath = new PathString(accessDeniedPath);
				   options.Cookies.ApplicationCookie.SlidingExpiration = slidingExpiration;
				   options.Cookies.ApplicationCookie.AutomaticChallenge = autmationChallange;
			   });
	}
}
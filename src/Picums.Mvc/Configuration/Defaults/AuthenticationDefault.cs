using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.Claims;
using Picums.Mvc.Authorization;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class AuthenticationDefault<TUser, TUserStore> : IDefault
        where TUser : class, IClaimsProvider, IUser
        where TUserStore : class, IUserStore<TUser>
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            host.Services.Add(this.ConfigureServices);
            host.ASP.Add(this.ConfigureApp);
            host.MVC.Filters.Add(this.BuildAuthorizeFilter());
        }

        private void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
            => app
                //.UseCookieAuthentication(new CookieAuthenticationOptions
                //{
                //    //AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                //    AuthenticationScheme = "Cookie",
                //    LoginPath = new PathString("/Login/Login/"),
                //    AccessDeniedPath = new PathString("/Login/Forbidden/"),
                //    AutomaticAuthenticate = true,
                //    AutomaticChallenge = true
                //    //Provider = new CookieAuthenticationProvider
                //    //{
                //    //    // Enables the application to validate the security stamp when the user logs in.
                //    //    // This is a security feature which is used when you change a password or add an external login to your account.
                //    //    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser>(
                //    //    validateInterval: TimeSpan.FromMinutes(30),
                //    //    regenerateIdentity: (manager, user) => user.GenerateUserIdentityAsync(manager))
                //    //}
                //})
                .UseIdentity();

        private void ConfigureServices(IServiceCollection services)
            => services
                .AddScoped<IUserStore<TUser>, TUserStore>()
                .AddScoped<IUserClaimsPrincipalFactory<TUser>, ClaimsPrincipalFactory<TUser>>()
                .AddScoped<IUserStore<TUser>, TUserStore>()
                .AddIdentity<TUser, string>();

        private AuthorizeFilter BuildAuthorizeFilter()
            => new AuthorizeFilter(
                new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new PermissionRequirement())
                    .Build());
    }
}
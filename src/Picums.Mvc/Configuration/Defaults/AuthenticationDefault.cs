using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
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
            //host.MVC.Filters.Add(new AuthorizeByPermissionFilter(arguments.ElementAt(0).ToString(), arguments.ElementAt(1).ToString()));
            host.MVC.Filters.Add(this.BuildAuthorizeFilter());
        }

        private void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
            => app
                .UseCookieAuthentication(new CookieAuthenticationOptions()
                {
                    LoginPath = new PathString("/login/login"),
                    AccessDeniedPath = new PathString("/login/login"),
                    LogoutPath = new PathString("/login/logout"),
                    AuthenticationScheme = "Cookies",
                    AutomaticAuthenticate = true,
                    AutomaticChallenge = true,
                    CookieSecure = CookieSecurePolicy.Always,
                    ExpireTimeSpan = TimeSpan.FromMinutes(5),
                    SlidingExpiration = true,
                })
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
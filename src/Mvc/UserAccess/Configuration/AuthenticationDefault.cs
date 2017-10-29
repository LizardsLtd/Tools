using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Picums.Data.Claims;
using Picums.Mvc.Configuration;
using Picums.Mvc.Configuration.Defaults;

namespace Picums.Mvc.UserAccess.Configuration
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

        private void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory lg)
            => app.UseAuthentication();

        private void ConfigureServices(IServiceCollection services)
        {
            services
                .AddScoped<IUserClaimsPrincipalFactory<TUser>, ClaimsPrincipalFactory<TUser>>()
                .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/login/login");
                    options.AccessDeniedPath = new PathString("/login/login");
                    options.LogoutPath = new PathString("/login/logout");
                    options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
                    options.SlidingExpiration = true;
                });
            services
                .AddIdentity<TUser, string>()
                .AddSignInManager<SignInManager<TUser>>()
                .AddDefaultTokenProviders()
                .AddUserStore<TUserStore>();
        }

        private AuthorizeFilter BuildAuthorizeFilter()
            => new AuthorizeFilter(
                new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new PermissionRequirement())
                    .Build());
    }
}
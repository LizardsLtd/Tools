using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Picums.Mvc.Configuration;
using Picums.Mvc.Configuration.Defaults;
using Picums.Mvc.UserAccess.Claims;
using Picums.Mvc.UserAccess.Stores;

namespace Picums.Mvc.UserAccess.Configuration
{
    public sealed class AuthenticationDefault<TUser> : IDefault
        where TUser : IdentityUser<Guid>, IUser
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
            //services
            //    .AddScoped<IUserClaimsPrincipalFactory<TUser>, ClaimsPrincipalFactory<TUser>>()
            //    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie(options =>
            //    {
            //        options.LoginPath = new PathString("/login/login");
            //        options.AccessDeniedPath = new PathString("/login/login");
            //        options.LogoutPath = new PathString("/login/logout");
            //        options.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //        options.SlidingExpiration = true;
            //    });
            //services
            //    .AddScoped<SignInManager<TUser>>()
            //    .AddScoped<IUserStore<TUser>, TUserStore>()
            //    .AddScoped<IUserClaimsPrincipalFactory<TUser>, ClaimsPrincipalFactory<TUser>>()
            //    .AddScoped<IUserStore<TUser>, TUserStore>();
            //services
            //    .AddIdentity<TUser, string>()
            //    .AddDefaultTokenProviders();
            //services
            //    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //    .AddCookie();
            //services
            //    .AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            //        .AddCookie(o =>
            //        {UserStore<
            //            o.LoginPath = new PathString("/login/login");
            //            o.AccessDeniedPath = new PathString("/login/login");
            //            o.LogoutPath = new PathString("/login/logout");
            //            o.CookieSecure = CookieSecurePolicy.Always;
            //            o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            //            o.SlidingExpiration = true;
            //        });
            services
                .AddScoped<IUserStore<TUser>, UserStore<TUser>>()
                .AddIdentity<TUser, string>()
                .AddDefaultTokenProviders();
        }

        private AuthorizeFilter BuildAuthorizeFilter()
            => new AuthorizeFilter(
                new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new PermissionRequirement())
                    .Build());
    }
}
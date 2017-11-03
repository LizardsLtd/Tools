using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.DependencyInjection;
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

        private void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
            => app.UseAuthentication();

        private void ConfigureServices(IServiceCollection services)
        {
            services
                .AddIdentity<TUser, string>()
                .AddDefaultTokenProviders();
            services
                .AddScoped<GetAllUsersDynamicQuery<TUser>>()
                .AddScoped<IUserStore<TUser>, UserStore<TUser>>();
        }

        private AuthorizeFilter BuildAuthorizeFilter()
            => new AuthorizeFilter(
                new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .AddRequirements(new PermissionRequirement())
                    .Build());
    }
}
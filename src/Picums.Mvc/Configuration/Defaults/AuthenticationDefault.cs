using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Picums.Data.Claims;
using Picums.Mvc.Claims.Entities;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class AuthenticationDefault<TUser, TUserStore> : BasicDefault
        where TUser : class, IClaimsProvider, IUser
        where TUserStore : class, IUserStore<TUser>
    {
        protected override void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env, IEnumerable<object> arguments)
        {
            app.UseIdentity();
        }

        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            services.AddIdentity<TUser, string>();
            services.AddTransient<IUserStore<TUser>, TUserStore>();
            services.AddScoped<IUserClaimsPrincipalFactory<TUser>, ClaimsPrincipalFactory<TUser>>();
            services.AddScoped<IUserStore<TUser>, TUserStore>();
        }
    }
}
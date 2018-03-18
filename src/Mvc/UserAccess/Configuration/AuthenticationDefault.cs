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
using Microsoft.Extensions.DependencyInjection.Extensions;
using Picums.Data.CQRS;
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
            host.MVC.Options.AddFilterMetadata(this.BuildAuthorizeFilter());
        }

        /// <summary>
        /// Adds and configures the identity system for the specified User and Role types.
        /// </summary>
        /// <typeparam name="TUser">The type representing a User in the system.</typeparam>
        /// <param name="services">The services available in the application.</param>
        /// <param name="setupAction">An action to configure the <see cref="IdentityOptions"/>.</param>
        /// <returns>An <see cref="IdentityBuilder"/> for creating and configuring the identity system.</returns>
        private IdentityBuilder AddIdentity(IServiceCollection services)
        {
            // Services used by identity
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
                options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
                options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
            })
            .AddCookie(IdentityConstants.ApplicationScheme, o =>
            {
                o.LoginPath = new PathString("/Account/Login");
                o.Events = new CookieAuthenticationEvents
                {
                    OnValidatePrincipal = SecurityStampValidator.ValidatePrincipalAsync,
                };
            })
            .AddCookie(IdentityConstants.ExternalScheme, o =>
            {
                o.Cookie.Name = IdentityConstants.ExternalScheme;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            })
            .AddCookie(
                IdentityConstants.TwoFactorRememberMeScheme,
                o => o.Cookie.Name = IdentityConstants.TwoFactorRememberMeScheme)
            .AddCookie(IdentityConstants.TwoFactorUserIdScheme, o =>
            {
                o.Cookie.Name = IdentityConstants.TwoFactorUserIdScheme;
                o.ExpireTimeSpan = TimeSpan.FromMinutes(5);
            });

            // Hosting doesn't add IHttpContextAccessor by default
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            // Identity services
            services.TryAddScoped<IUserValidator<TUser>, UserValidator<TUser>>();
            services.TryAddScoped<IPasswordValidator<TUser>, PasswordValidator<TUser>>();
            services.TryAddScoped<IPasswordHasher<TUser>, PasswordHasher<TUser>>();
            services.TryAddScoped<ILookupNormalizer, UpperInvariantLookupNormalizer>();
            // No interface for the error describer so we can add errors without rev'ing the interface
            services.TryAddScoped<IdentityErrorDescriber>();
            services.TryAddScoped<ISecurityStampValidator, SecurityStampValidator<TUser>>();
            services.TryAddScoped<IUserClaimsPrincipalFactory<TUser>, UserClaimsPrincipalFactory<TUser>>();
            services.TryAddScoped<UserManager<TUser>, AspNetUserManager<TUser>>();
            services.TryAddScoped<SignInManager<TUser>, SignInManager<TUser>>();

            services
                .AddTransient<ICommandHandler, CreateUserCommandHandler<TUser>>()
                .AddSingleton<IClaimProvider, NameClaimProvider>()
                .AddSingleton<IClaimProvider, PermissionClaimProvider>();

            return new IdentityBuilder(typeof(TUser), services);
        }

        private void ConfigureApp(IApplicationBuilder app, IHostingEnvironment env)
                    => app.UseAuthentication();

        private void ConfigureServices(IServiceCollection services)
        {
            this
                .AddIdentity(services)
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
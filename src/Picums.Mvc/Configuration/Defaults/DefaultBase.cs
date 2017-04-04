using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Picums.Mvc.Configuration.Defaults
{
    public abstract class DefaultBase : IDefault
    {
        public virtual void Apply(StartupConfigurations host, params object[] arguments)
        {
            throw new NotImplementedException();
        }

        protected abstract void AddConfiguration(IApplicationBuilder app, IHostingEnvironment env);
        protected abstract void AddServices(IServiceCollectin app, IHostingEnvironment env);
        startup.AddServices(services
                => services
                    .AddClaimsIdentity<TUser>()
                    .AddScoped<IUserStore<TUser>, TUserStore>());
    }
}

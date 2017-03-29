using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DevelopmentSetup : IDefault
    {
        public void Apply(StartupConfigurations host, params object[] arguments)
        {
            if (host.Environment.IsDevelopment())
            {
                host.ASP.Add((app, env) => app.UseDeveloperExceptionPage());
                host.ASP.Add((app, env) => app.UseBrowserLink());
            }
        }
    }
}
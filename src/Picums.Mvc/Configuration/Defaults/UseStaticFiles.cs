using Microsoft.AspNetCore.Builder;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class UseStaticFiles : IDefault
    {
        public void Apply(StartupConfigurations host, params object[] arguments)
        {
            host.ASP.Add((app, env) => app.UseStaticFiles());
        }
    }
}
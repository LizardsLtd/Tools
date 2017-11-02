using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class MiddlewareDefault<TMiddleware> : BasicDefault
    {
        protected override void ConfigureApp(
                IApplicationBuilder app
                , IHostingEnvironment env
                , IEnumerable<object> arguments)
        {
            if (arguments.Any())
            {
                app.UseMiddleware<TMiddleware>(arguments);
            }
            else
            {
                app.UseMiddleware<TMiddleware>();
            }
        }
    }
}
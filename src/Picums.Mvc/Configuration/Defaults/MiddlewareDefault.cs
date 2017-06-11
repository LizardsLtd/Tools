using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class MiddlewareDefault<TMiddleware> : BasicDefault
    {
        protected override void ConfigureApp(
                IApplicationBuilder app
                , IHostingEnvironment env
                , ILoggerFactory lg
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
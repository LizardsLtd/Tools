using System.Collections.Generic;
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
            app.UseMiddleware<TMiddleware>(arguments);
        }
    }
}
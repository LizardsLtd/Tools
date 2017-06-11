using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class UseStaticFiles : BasicDefault
    {
        protected override void ConfigureApp(
                IApplicationBuilder app
                , IHostingEnvironment env
                , ILoggerFactory lg
                , IEnumerable<object> arguments)
            => app.UseStaticFiles();
    }
}
using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DevelopmentSetup : BasicDefault
    {
        protected override void ConfigureApp(
            IApplicationBuilder app
            , IHostingEnvironment env
            , ILoggerFactory loggerFactory
            , IEnumerable<object> arguments)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
        }
    }
}
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class AspRegistry
    {
        private readonly List<Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory>> actions;

        public AspRegistry()
        {
            this.actions = new List<Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory>>();
        }

        internal void Configure(IApplicationBuilder app, IHostingEnvironment environment, ILoggerFactory loggerFactory)
        {
            this.actions.ForEach(x => x(app, environment, loggerFactory));
        }
    }
}
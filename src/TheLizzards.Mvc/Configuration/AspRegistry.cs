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
            actions = new List<Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory>>();
        }

        public AspRegistry Configure(Action<IApplicationBuilder, IHostingEnvironment, ILoggerFactory> action)
        {
            actions.Add(action);

            return this;
        }

        internal void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
            => actions.ForEach(x => x(app, env, loggerFactory));
    }
}
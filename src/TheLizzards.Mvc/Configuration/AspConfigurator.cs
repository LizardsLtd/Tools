using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class AspConfigurator
    {
        private readonly List<Action<IApplicationBuilder, IHostingEnvironment>> actions;

        public AspConfigurator()
        {
            this.actions = new List<Action<IApplicationBuilder, IHostingEnvironment>>();
        }

        internal void Add(Action<IApplicationBuilder, IHostingEnvironment> action)
            => this.actions.Add(action);

        internal void Use(IApplicationBuilder app, IHostingEnvironment environment)
        {
            this.actions.ForEach(x => x(app, environment));
        }
    }
}
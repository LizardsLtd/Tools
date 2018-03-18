using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using ASPConfigAction
    = System.Action
        <Microsoft.AspNetCore.Builder.IApplicationBuilder
        , Microsoft.AspNetCore.Hosting.IHostingEnvironment>;

namespace Picums.Mvc.Configuration
{
    public sealed class AspConfigurator
    {
        private readonly List<ASPConfigAction> actions;

        public AspConfigurator()
        {
            this.actions = new List<ASPConfigAction>();
        }

        public void Add(ASPConfigAction action)
            => this.actions.Add(action);

        internal void Use(IApplicationBuilder app, IHostingEnvironment environment)
        {
            this.actions.ForEach(x => x(app, environment));
        }
    }
}
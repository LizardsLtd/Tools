using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;

namespace TheLizzards.Mvc.Configuration
{
    public sealed class RouteConfigurator
    {
        private readonly List<Action<IRouteBuilder>> routeTemplates;

        internal RouteConfigurator()
        {
            this.routeTemplates = new List<Action<IRouteBuilder>>();
        }

        public RouteConfigurator AddRoute(Action<IRouteBuilder> action)
        {
            this.routeTemplates.Add(action);
            return this;
        }

        internal void BuildRoutes(IRouteBuilder routes)
            => this.routeTemplates.ForEach(x => x(routes));
    }
}
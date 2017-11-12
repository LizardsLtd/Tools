using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Routing;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class RouteDefault : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
            => arguments
                .Cast<Action<IRouteBuilder>>()
                .ToList()
                .ForEach(routeAction => host.MVC.Routes.Add(routeAction));
    }
}
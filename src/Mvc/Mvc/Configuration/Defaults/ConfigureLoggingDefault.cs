using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Logging;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class ConfigureLoggingDefault : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            var actions = arguments.Cast<Action<ILoggerFactory>>().ToList();

            host.ASP.Add((app, env, loggerFactory) => actions.ForEach(x => x(loggerFactory)));
        }
    }
}
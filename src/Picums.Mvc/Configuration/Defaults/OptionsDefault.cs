using System;
using System.Collections.Generic;
using System.Linq;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class OptionsDefault<TOptions> : IDefault
        where TOptions : class
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            if (arguments.FirstOrDefault() is Action<TOptions> configure)
            {
                host.Services.Configure(configure);
            }
        }
    }
}
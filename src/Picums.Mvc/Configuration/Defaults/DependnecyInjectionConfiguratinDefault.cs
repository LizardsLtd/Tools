using System;
using System.Collections.Generic;
using System.Linq;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DependnecyInjectionConfiguratinDefault<TOption> : IDefault
        where TOption : class
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            if (arguments.FirstOrDefault() is Action<TOption> option)
            {
                host.Services.Configure(option);
            }
        }
    }
}
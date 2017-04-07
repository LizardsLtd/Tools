using System.Collections.Generic;
using System.Linq;

namespace Picums.Mvc.Configuration.Defaults
{
    public abstract class ConfigurableDefault<TOption> : IDefault
    {
        public void Apply(StartupConfigurations host, IEnumerable<object> arguments)
        {
            if (arguments.FirstOrDefault() is TOption option)
            {
                this.Apply(host, option);
            }
        }

        protected abstract void Apply(StartupConfigurations host, TOption option);
    }
}
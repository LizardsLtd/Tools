using System.Collections.Generic;

namespace Picums.Mvc.Configuration.Defaults
{
    public interface IDefault
    {
        void Apply(StartupConfigurations host, IEnumerable<object> arguments);
    }
}
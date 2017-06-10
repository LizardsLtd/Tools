using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DependencyInjectionDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
            => arguments
                .Cast<Action<IServiceCollection>>()
                .ToList()
                .ForEach(x => x(services));
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class DependencyInjectionDefault : BasicDefault
    {
        private readonly List<Action<IServiceCollection>> configurationActions;

        public DependencyInjectionDefault(params Action<IServiceCollection>[] configurationActions)
        {
            this.configurationActions = configurationActions.ToList();
        }

        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
            => configurationActions.ForEach(x => x(services));
    }
}
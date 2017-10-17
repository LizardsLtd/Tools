using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Picums.Data.CQRS;
using Picums.Data.Events;

namespace Picums.Mvc.Configuration.Defaults
{
    public sealed class InMemoryCQRSBusesDefault : BasicDefault
    {
        protected override void ConfigureServices(IServiceCollection services, IEnumerable<object> arguments)
        {
            services.TryAddSingleton<ICommandBus, CommandBus>();
            services.TryAddSingleton<IEventBus, EventBus>();
        }
    }
}
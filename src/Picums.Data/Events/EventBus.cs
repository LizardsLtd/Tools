using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picums.Data.Events
{
    public sealed class EventBus : IEventBus
    {
        private readonly IEnumerable<IEventHandler> handlers;

        public EventBus(IEnumerable<IEventHandler> handlers)
        {
            this.handlers = handlers;
        }

        public Task Publish<TEvent>(TEvent @event) where TEvent : IEvent
            => Task.Factory.StartNew(()
                => ExecuteHandlers(@event));

        private ParallelLoopResult ExecuteHandlers<TEvent>(TEvent @event)
                where TEvent : IEvent
            => Parallel.ForEach(
                this.GetHandlesForEvent<TEvent>(),
                handler => handler.Handle(@event));

        private IEnumerable<IEventHandler<TEvent>> GetHandlesForEvent<TEvent>()
                where TEvent : IEvent
            => this.handlers
                .Where(x => x is IEventHandler<TEvent>)
                .Cast<IEventHandler<TEvent>>();
    }
}
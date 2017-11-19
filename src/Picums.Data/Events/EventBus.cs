using System;
using System.Threading.Tasks;

namespace Picums.Data.Events
{

    public sealed class EventBus : IEventBus
    {
        public Task Publish<T>(T @event) where T : IEvent
        {
            throw new NotImplementedException();
        }

        public Task Subscribe<T>(Action<T> subscriber) where T : IEvent
        {
            throw new NotImplementedException();
        }
    }
}
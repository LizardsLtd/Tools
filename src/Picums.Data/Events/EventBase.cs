using System;

namespace Picums.Data.Events
{

    public abstract class EventBase : IEvent
    {
        public EventBase()
            => this.EventId = Guid.NewGuid();

        public Guid EventId { get; }
    }
}
using System;

namespace Picums.Data.Events
{
    public abstract class EventBase : IEvent
    {
        public Guid EventId { get; } = Guid.NewGuid();
    }
}
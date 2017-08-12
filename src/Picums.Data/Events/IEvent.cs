using System;

namespace Picums.Data.Events
{
    public interface IEvent
    {
        Guid EventId { get; }
    }
}
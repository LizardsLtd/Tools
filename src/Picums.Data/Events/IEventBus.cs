using System;
using System.Threading.Tasks;

namespace Picums.Data.Events
{
    public interface IEventBus : IDisposable
    {
        Task Publish<TEvent>(TEvent @event) where TEvent : IEvent;
    }
}
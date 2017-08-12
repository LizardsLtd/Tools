namespace Picums.Data.Events
{
    public interface IEventHandler
    {
    }

    public interface IEventHandler<TEvent> : IEventHandler
        where TEvent : IEvent
    {
        void Handle(TEvent @event);
    }
}
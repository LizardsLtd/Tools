using Picums.Data.Events;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class UserCreationProcessFinishedEvent : EventBase
    {
        public UserCreationProcessFinishedEvent(string username, bool isSucessful)
        {
            this.Username = username;
            this.IsSucessful = isSucessful;
        }

        public string Username { get; }

        public bool IsSucessful { get; }
    }
}
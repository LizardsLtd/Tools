using Microsoft.AspNetCore.Identity;
using Picums.Data.Events;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class UserCreationProcessFinishedEvent : EventBase
    {
        public UserCreationProcessFinishedEvent(params IdentityError[] errors)
        {
            this.IdentityResult = errors.Length == 0
                ? IdentityResult.Success
                : IdentityResult.Failed(errors);
        }

        public IdentityResult IdentityResult { get; }
    }
}
using System;
using Microsoft.AspNetCore.Identity;
using Picums.Data.CQRS;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class CreateUserCommand<TUser> : CommandBase
        where TUser : IdentityUser<Guid>, IUser
    {
        public CreateUserCommand(TUser user)
        {
            this.User = user;
        }

        public TUser User { get; }
    }
}
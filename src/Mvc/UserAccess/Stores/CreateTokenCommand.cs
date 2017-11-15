using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{

    public sealed class CreateTokenCommand : CommandBase
    {
        public CreateTokenCommand(AggregateUserToken token)
        {
            this.Token = token;
        }

        public AggregateUserToken Token { get; }
    }
}
using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{

    public sealed class CreateLoginCommand : CommandBase
    {
        public CreateLoginCommand(AggregateUserLogin login)
        {
            this.Login = login;
        }

        public AggregateUserLogin Login { get; }
    }
}
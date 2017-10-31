using System;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;
using Picums.Maybe;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class FindUserByIdQuery<TUser> : QueryProvider<Maybe<TUser>>, IsQuery
        where TUser : IdentityUser<Guid>, IUser
    {
        public FindUserByIdQuery(IDataContext dataContext, ILogger logger, DatabaseParts userParts)
            : base(dataContext, logger, userParts)
        {
        }

        public IAsyncQuery<Maybe<TUser>> GetQuery(Guid id)
            => new QueryById<TUser>(this.dataContext, this.logger, this.parts, id);
    }
}
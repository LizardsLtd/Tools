using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.Claims;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;
using Picums.Maybe;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class FindUserByNameQuery<TUser> : QueryProvider<Maybe<TUser>>, IsQuery
        where TUser : IdentityUser<Guid>, IUser
    {
        public FindUserByNameQuery(IDataContext dataContext, ILogger logger, DatabaseParts userParts)
            : base(dataContext, logger, userParts)
        {
        }

        public IAsyncQuery<IEnumerable<TUser>> GetQuery(string name)
            => new QueryBySpecification<TUser>(
                this.dataContext,
                this.logger,
                this.parts,
                user => user.NormalizedUserName == name);
    }
}
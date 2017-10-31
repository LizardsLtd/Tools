using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Types;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class UserExistsQuery<TUser> : IsQuery
        where TUser : IdentityUser<Guid>, IUser
    {
        private readonly IDataContext storageContext;
        private readonly ILogger logger;

        public UserExistsQuery(
            IDataContext storageContext,
            ILogger logger)
        {
            this.storageContext = storageContext;
            this.logger = logger;
        }

        public IAsyncQuery<bool> SetEmail(Email email)
            => new Query(this.storageContext, this.logger, email.Address);

        private sealed class Query : IAsyncQuery<bool>
        {
            private readonly IDataContext storageContext;
            private readonly ILogger logger;
            private readonly string username;

            public Query(IDataContext storageContext, ILogger logger, string username)
            {
                this.storageContext = storageContext;
                this.logger = logger;
                this.username = username;
            }

            public async Task<bool> Execute()
                => (await new FindUserByNameQuery<TUser>(this.storageContext, this.logger)
                    .GetQuery(this.username)
                    .Execute())
                    .Any();
        }
    }
}
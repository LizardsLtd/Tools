using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.CQRS.Queries;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class GetAllUsersDynamicQuery<TUser> : QueryProvider<IQueryable<TUser>>, IsQuery
        where TUser : IdentityUser<Guid>, IUser
    {
        public GetAllUsersDynamicQuery(IDataContext dataContext, ILogger logger)
            : base(dataContext, logger)
        {
        }

        public async Task<IQueryable<TUser>> Execute()
            => await new QueryForAllBuilder<TUser>()
                .WithDataContext(this.DataContext)
                .WithLogger(this.Logger)
                .Execute();
    }
}
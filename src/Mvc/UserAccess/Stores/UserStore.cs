using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.Events;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class UserStore<TUser>
            : UserStoreBase<TUser, Guid, AggregateUserClaim, AggregateUserLogin, AggregateUserToken>,
            IUserStore<TUser>
        where TUser : IdentityUser<Guid>, IUser
    {
        private readonly ILogger logger;
        private readonly GetAllUsersDynamicQuery<TUser> userQuery;
        private readonly ICommandBus commandBus;
        private readonly IEventBus eventBus;
        private readonly IEnumerable<IClaimProvider> claimProviders;

        public UserStore(
                IdentityErrorDescriber describer,
                ILogger logger,
                GetAllUsersDynamicQuery<TUser> userQuery,
                ICommandBus commandBus,
                IEventBus eventBus,
                IEnumerable<IClaimProvider> claimProviders)
            : base(describer)
        {
            this.logger = logger;
            this.userQuery = userQuery;
            this.commandBus = commandBus;
            this.eventBus = eventBus;
            this.claimProviders = claimProviders.ToList();
        }

        public override IQueryable<TUser> Users => throw new NotImplementedException();

        public override Task AddClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task AddLoginAsync(TUser user, UserLoginInfo login, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async override Task<IdentityResult> CreateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            var command = new CreateUserCommand<TUser>(user);
            var eventBusReciever = EventBusSynchronusConverter<UserCreationProcessFinishedEvent>.Setup(this.eventBus);

            await this.commandBus.Execute(command);

            var response = await eventBusReciever.GetResult();

            return response.IdentityResult;
        }

        public override Task<IdentityResult> DeleteAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public async override Task<TUser> FindByEmailAsync(string normalizedEmail, CancellationToken cancellationToken = default(CancellationToken))
            => (await this.userQuery.Execute())
                .FirstOrDefault(user => string.Equals(user.NormalizedEmail, normalizedEmail, StringComparison.OrdinalIgnoreCase));

        public async override Task<TUser> FindByIdAsync(string userId, CancellationToken cancellationToken = default(CancellationToken))
            => (await this.userQuery.Execute())
                .FirstOrDefault(user => user.Id.Equals(userId));

        public async override Task<TUser> FindByNameAsync(string normalizedUserName, CancellationToken cancellationToken = default(CancellationToken))
            => (await this.userQuery.Execute())
                .FirstOrDefault(user => string.Equals(user.NormalizedUserName, normalizedUserName, StringComparison.OrdinalIgnoreCase));

        public override Task<IList<Claim>> GetClaimsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
            => Task.FromResult<IList<Claim>>(
                this.claimProviders
                    .SelectMany(provider => provider.GetClaims(user))
                    .ToList());

        public override Task<IList<UserLoginInfo>> GetLoginsAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task<IList<TUser>> GetUsersForClaimAsync(Claim claim, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task RemoveClaimsAsync(TUser user, IEnumerable<Claim> claims, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task RemoveLoginAsync(TUser user, string loginProvider, string providerKey, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task ReplaceClaimAsync(TUser user, Claim claim, Claim newClaim, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> UpdateAsync(TUser user, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new NotImplementedException();
        }

        protected override Task AddUserTokenAsync(AggregateUserToken token)
        {
            throw new NotImplementedException();
        }

        protected override Task<AggregateUserToken> FindTokenAsync(TUser user, string loginProvider, string name, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<TUser> FindUserAsync(Guid userId, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<AggregateUserLogin> FindUserLoginAsync(Guid userId, string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task<AggregateUserLogin> FindUserLoginAsync(string loginProvider, string providerKey, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        protected override Task RemoveUserTokenAsync(AggregateUserToken token)
        {
            throw new NotImplementedException();
        }
    }
}
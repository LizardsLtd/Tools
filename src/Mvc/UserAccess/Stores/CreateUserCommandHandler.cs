using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using NLog;
using Picums.Data.CQRS;
using Picums.Data.CQRS.DataAccess;
using Picums.Data.Events;
using Picums.Mvc.UserAccess.Claims;

namespace Picums.Mvc.UserAccess.Stores
{
    public sealed class CreateUserCommandHandler<TUser>
        : ICommandHandler<CreateUserCommand<TUser>>,
        ICommandHandler<CreateLoginCommand>,
        ICommandHandler<CreateTokenCommand>
            where TUser : IdentityUser<Guid>, IUser
    {
        private readonly IDataContext storageContext;
        private readonly IEventBus eventBus;
        private readonly ILogger logger;

        public CreateUserCommandHandler(IDataContext storageContext, IEventBus eventBus, ILogger logger)
        {
            this.storageContext = storageContext;
            this.eventBus = eventBus;
            this.logger = logger;
        }

        public void Dispose()
        {
        }

        public async Task Handle(CreateUserCommand<TUser> command)
        {
            try
            {
                await this.storageContext
                    .GetWriter<TUser>()
                    .InsertNew(command.User);

                await this.eventBus.Publish(new UserCreationProcessFinishedEvent());
            }
            catch (Exception exp)
            {
                await this.eventBus.Publish(new ExceptionEvent(exp, "Could not create new user"));
                await this.eventBus.Publish(new UserCreationProcessFinishedEvent(new IdentityError()));
            }
        }

        public async Task Handle(CreateLoginCommand command)
        {
            await this.storageContext
                .GetWriter<AggregateUserLogin>()
                .InsertNew(command.Login);
        }

        public async Task Handle(CreateTokenCommand command)
        {
            await this.storageContext
                .GetWriter<AggregateUserToken>()
                .InsertNew(command.Token);
        }
    }
}